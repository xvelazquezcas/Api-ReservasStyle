using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class ServicioPromocionService : IServicioPromocionService
    {
        private readonly IServicioPromocionRepository _servicioPromocionRepository;
        private readonly IPromocionesRepository _promocionesRepository;
        private readonly IMapper _mapper;

        public ServicioPromocionService(
            IServicioPromocionRepository servicioPromocionRepository,
            IPromocionesRepository promocionesRepository,
            IMapper mapper)
        {
            _servicioPromocionRepository = servicioPromocionRepository;
            _promocionesRepository = promocionesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una asociación servicio-promoción específica
        /// </summary>
        public async Task<ServicioPromocionResponseDto> GetAsync(int idServicioSucursal, int idPromocion)
        {
            var servicioPromocion = await _servicioPromocionRepository.GetAsync(idServicioSucursal, idPromocion);
            if (servicioPromocion == null)
                throw new KeyNotFoundException(
                    $"Asociación entre ServicioSucursal {idServicioSucursal} y Promoción {idPromocion} no encontrada");

            return _mapper.Map<ServicioPromocionResponseDto>(servicioPromocion);
        }

        /// <summary>
        /// Obtiene todas las asociaciones servicio-promoción
        /// </summary>
        public async Task<IEnumerable<ServicioPromocionResponseDto>> GetAllAsync()
        {
            var asociaciones = await _servicioPromocionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServicioPromocionResponseDto>>(asociaciones);
        }

        /// <summary>
        /// Crea una nueva asociación servicio-promoción
        /// </summary>
        public async Task<ServicioPromocionResponseDto> CreateAsync(CrearServicioPromocionDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            // Verificar que la promoción existe
            var promocion = await _promocionesRepository.GetByIdAsync(dto.IdPromocion);
            if (promocion == null)
                throw new KeyNotFoundException(
                    $"Promoción con ID {dto.IdPromocion} no encontrada");

            // Verificar que no existe ya la asociación
            var existe = await _servicioPromocionRepository.ExisteAsync(
                dto.IdServicioSucursal, dto.IdPromocion);
            if (existe)
                throw new InvalidOperationException(
                    $"Ya existe una asociación entre ServicioSucursal {dto.IdServicioSucursal} " +
                    $"y Promoción {dto.IdPromocion}");

            // ✅ MAPEO DTO → ENTIDAD
            var servicioPromocion = _mapper.Map<ServicioPromocion>(dto);

            // ✅ PERSISTENCIA
            await _servicioPromocionRepository.CreateAsync(servicioPromocion);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<ServicioPromocionResponseDto>(servicioPromocion);
        }

        /// <summary>
        /// Actualiza una asociación servicio-promoción
        /// </summary>
        public async Task<ServicioPromocionResponseDto> UpdateAsync(
            int idServicioSucursal, int idPromocionAnterior,
            ActualizarServicioPromocionDto dto)
        {
            var servicioPromocion = await _servicioPromocionRepository.GetAsync(
                idServicioSucursal, idPromocionAnterior);
            if (servicioPromocion == null)
                throw new KeyNotFoundException(
                    $"Asociación entre ServicioSucursal {idServicioSucursal} " +
                    $"y Promoción {idPromocionAnterior} no encontrada");

            // ✅ VALIDAR SI LA PROMOCIÓN CAMBIÓ
            if (idPromocionAnterior != dto.IdPromocion)
            {
                // Verificar que la nueva promoción existe
                var nueva = await _promocionesRepository.GetByIdAsync(dto.IdPromocion);
                if (nueva == null)
                    throw new KeyNotFoundException(
                        $"Promoción con ID {dto.IdPromocion} no encontrada");

                // Verificar que no existe ya esa asociación
                var existe = await _servicioPromocionRepository.ExisteAsync(
                    dto.IdServicioSucursal, dto.IdPromocion);
                if (existe)
                    throw new InvalidOperationException(
                        $"Ya existe una asociación entre ServicioSucursal {dto.IdServicioSucursal} " +
                        $"y Promoción {dto.IdPromocion}");
            }

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, servicioPromocion);

            // ✅ PERSISTENCIA
            await _servicioPromocionRepository.UpdateAsync(servicioPromocion);

            return _mapper.Map<ServicioPromocionResponseDto>(servicioPromocion);
        }

        /// <summary>
        /// Elimina una asociación servicio-promoción
        /// </summary>
        public async Task<bool> DeleteAsync(int idServicioSucursal, int idPromocion)
        {
            var servicioPromocion = await _servicioPromocionRepository.GetAsync(
                idServicioSucursal, idPromocion);
            if (servicioPromocion == null)
                throw new KeyNotFoundException(
                    $"Asociación entre ServicioSucursal {idServicioSucursal} " +
                    $"y Promoción {idPromocion} no encontrada");

            await _servicioPromocionRepository.DeleteAsync(idServicioSucursal, idPromocion);
            return true;
        }

        /// <summary>
        /// Obtiene todas las promociones de un servicio (con detalles)
        /// </summary>
        public async Task<IEnumerable<ServicioPromocionDetalleDto>> GetPromocionesDeServicioAsync(int idServicio)
        {
            var asociaciones = await _servicioPromocionRepository.GetByIdServicioAsync(idServicio);
            if (!asociaciones.Any())
                throw new KeyNotFoundException(
                    $"ServicioSucursal con ID {idServicio} no encontrado o no tiene promociones asociadas");

            return _mapper.Map<IEnumerable<ServicioPromocionDetalleDto>>(asociaciones);
        }

        /// <summary>
        /// Obtiene todos los servicios de una promoción
        /// </summary>
        public async Task<IEnumerable<ServicioPromocionResponseDto>> GetServiciosDePromocionAsync(int idPromocion)
        {
            var promocion = await _promocionesRepository.GetByIdAsync(idPromocion);
            if (promocion == null)
                throw new KeyNotFoundException(
                    $"Promoción con ID {idPromocion} no encontrada");

            var asociaciones = await _servicioPromocionRepository.GetByIdPromocionAsync(idPromocion);
            return _mapper.Map<IEnumerable<ServicioPromocionResponseDto>>(asociaciones);
        }

        /// <summary>
        /// Obtiene el conteo de promociones de un servicio
        /// </summary>
        public async Task<int> GetCountPromocionesDeServicioAsync(int idServicio)
        {
            return await _servicioPromocionRepository.GetCountByServicioAsync(idServicio);
        }

        /// <summary>
        /// Obtiene el conteo de servicios de una promoción
        /// </summary>
        public async Task<int> GetCountServiciosDePromocionAsync(int idPromocion)
        {
            var promocion = await _promocionesRepository.GetByIdAsync(idPromocion);
            if (promocion == null)
                throw new KeyNotFoundException(
                    $"Promoción con ID {idPromocion} no encontrada");

            return await _servicioPromocionRepository.GetCountByPromocionAsync(idPromocion);
        }
    }
}
