using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class ServicioSucursalService : IServicioSucursalService
    {
        private readonly IServicioSucursalRepository _servicioSucursalRepository;
        private readonly IServicioRepository _servicioRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IMapper _mapper;

        public ServicioSucursalService(
            IServicioSucursalRepository servicioSucursalRepository,
            IServicioRepository servicioRepository,
            ISucursalRepository sucursalRepository,
            IMapper mapper)
        {
            _servicioSucursalRepository = servicioSucursalRepository;
            _servicioRepository = servicioRepository;
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los servicios-sucursales
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> GetAllAsync()
        {
            var servicios = await _servicioSucursalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(servicios);
        }

        /// <summary>
        /// Obtiene un servicio-sucursal por ID
        /// </summary>
        public async Task<ServicioSucursalResponseDto> GetByIdAsync(int idServicioSucursal)
        {
            var servicioSucursal = await _servicioSucursalRepository.GetByIdAsync(idServicioSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException($"ServicioSucursal con ID {idServicioSucursal} no encontrado");

            return _mapper.Map<ServicioSucursalResponseDto>(servicioSucursal);
        }

        /// <summary>
        /// Crea un nuevo servicio-sucursal
        /// </summary>
        public async Task<ServicioSucursalResponseDto> CreateAsync(CrearServicioSucursalDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            // Verificar que el servicio existe
            var servicio = await _servicioRepository.GetByIdAsync(dto.IdServicio);
            if (servicio == null)
                throw new KeyNotFoundException($"Servicio con ID {dto.IdServicio} no encontrado");

            // Verificar que la sucursal existe
            var sucursal = await _sucursalRepository.GetByIdAsync(dto.IdSucursal);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {dto.IdSucursal} no encontrada");

            // Verificar que no existe ya la asociación
            var existe = await _servicioSucursalRepository.ExisteAsync(dto.IdServicio, dto.IdSucursal);
            if (existe)
                throw new InvalidOperationException(
                    $"Ya existe una asociación entre el Servicio {dto.IdServicio} " +
                    $"y la Sucursal {dto.IdSucursal}");

            // ✅ Validar precio
            if (dto.Precio <= 0)
                throw new InvalidOperationException("El precio debe ser mayor a 0");

            // ✅ MAPEO DTO → ENTIDAD
            var servicioSucursal = _mapper.Map<ServicioSucursal>(dto);

            // ✅ PERSISTENCIA
            await _servicioSucursalRepository.CreateAsync(servicioSucursal);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<ServicioSucursalResponseDto>(servicioSucursal);
        }

        /// <summary>
        /// Actualiza un servicio-sucursal
        /// </summary>
        public async Task<ServicioSucursalResponseDto> UpdateAsync(int idServicioSucursal, ActualizarServicioSucursalDto dto)
        {
            var servicioSucursal = await _servicioSucursalRepository.GetByIdAsync(idServicioSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException($"ServicioSucursal con ID {idServicioSucursal} no encontrado");

            // ✅ VALIDAR SI EL SERVICIO CAMBIÓ
            if (servicioSucursal.IdServicio != dto.IdServicio)
            {
                var servicio = await _servicioRepository.GetByIdAsync(dto.IdServicio);
                if (servicio == null)
                    throw new KeyNotFoundException($"Servicio con ID {dto.IdServicio} no encontrado");

                // Verificar que no existe ya esa asociación
                var existe = await _servicioSucursalRepository.ExisteAsync(
                    dto.IdServicio, servicioSucursal.IdSucursal);
                if (existe)
                    throw new InvalidOperationException(
                        $"Ya existe una asociación entre el Servicio {dto.IdServicio} " +
                        $"y la Sucursal {servicioSucursal.IdSucursal}");
            }

            // ✅ VALIDAR SI LA SUCURSAL CAMBIÓ
            if (servicioSucursal.IdSucursal != dto.IdSucursal)
            {
                var sucursal = await _sucursalRepository.GetByIdAsync(dto.IdSucursal);
                if (sucursal == null)
                    throw new KeyNotFoundException($"Sucursal con ID {dto.IdSucursal} no encontrada");

                // Verificar que no existe ya esa asociación
                var existe = await _servicioSucursalRepository.ExisteAsync(
                    servicioSucursal.IdServicio, dto.IdSucursal);
                if (existe)
                    throw new InvalidOperationException(
                        $"Ya existe una asociación entre el Servicio {servicioSucursal.IdServicio} " +
                        $"y la Sucursal {dto.IdSucursal}");
            }

            // ✅ Validar precio
            if (dto.Precio <= 0)
                throw new InvalidOperationException("El precio debe ser mayor a 0");

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, servicioSucursal);

            // ✅ PERSISTENCIA
            await _servicioSucursalRepository.UpdateAsync(servicioSucursal);

            return _mapper.Map<ServicioSucursalResponseDto>(servicioSucursal);
        }

        /// <summary>
        /// Elimina un servicio-sucursal
        /// </summary>
        public async Task<bool> DeleteAsync(int idServicioSucursal)
        {
            var servicioSucursal = await _servicioSucursalRepository.GetByIdAsync(idServicioSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException($"ServicioSucursal con ID {idServicioSucursal} no encontrado");

            await _servicioSucursalRepository.DeleteAsync(idServicioSucursal);
            return true;
        }

        /// <summary>
        /// Obtiene un servicio-sucursal con detalles completos
        /// </summary>
        public async Task<ServicioSucursalDetalleDto> GetDetalleAsync(int idServicio, int idSucursal)
        {
            var servicioSucursal = await _servicioSucursalRepository.GetByServicioAndSucursalAsync(idServicio, idSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException(
                    $"Asociación entre Servicio {idServicio} y Sucursal {idSucursal} no encontrada");

            return _mapper.Map<ServicioSucursalDetalleDto>(servicioSucursal);
        }

        /// <summary>
        /// Obtiene todos los servicios de una sucursal
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalDetalleDto>> GetServiciosDeSucursalAsync(int idSucursal)
        {
            var sucursal = await _sucursalRepository.GetByIdAsync(idSucursal);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {idSucursal} no encontrada");

            var servicios = await _servicioSucursalRepository.GetBySucursalAsync(idSucursal);
            return _mapper.Map<IEnumerable<ServicioSucursalDetalleDto>>(servicios);
        }

        /// <summary>
        /// Obtiene todas las sucursales donde está disponible un servicio
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> GetSucursalesDelServicioAsync(int idServicio)
        {
            var servicio = await _servicioRepository.GetByIdAsync(idServicio);
            if (servicio == null)
                throw new KeyNotFoundException($"Servicio con ID {idServicio} no encontrado");

            var asociaciones = await _servicioSucursalRepository.GetByServicioAsync(idServicio);
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(asociaciones);
        }

        /// <summary>
        /// Obtiene todos los servicios-sucursales activos
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> GetActivosAsync()
        {
            var servicios = await _servicioSucursalRepository.GetActivosAsync();
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(servicios);
        }

        /// <summary>
        /// Obtiene todos los servicios activos de una sucursal
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> GetActivosBySucursalAsync(int idSucursal)
        {
            var sucursal = await _sucursalRepository.GetByIdAsync(idSucursal);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {idSucursal} no encontrada");

            var servicios = await _servicioSucursalRepository.GetActivosBySucursalAsync(idSucursal);
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(servicios);
        }

        /// <summary>
        /// Obtiene el precio de un servicio en una sucursal
        /// </summary>
        public async Task<decimal> GetPrecioAsync(int idServicio, int idSucursal)
        {
            var precio = await _servicioSucursalRepository.GetPrecioAsync(idServicio, idSucursal);
            if (precio == null)
                throw new KeyNotFoundException(
                    $"No existe precio para Servicio {idServicio} en Sucursal {idSucursal}");

            return precio.Value;
        }

        /// <summary>
        /// Actualiza el precio de un servicio en una sucursal
        /// </summary>
        public async Task<ServicioSucursalResponseDto> UpdatePrecioAsync(int idServicio, int idSucursal, decimal nuevoPrecio)
        {
            if (nuevoPrecio <= 0)
                throw new InvalidOperationException("El precio debe ser mayor a 0");

            var servicioSucursal = await _servicioSucursalRepository.GetByServicioAndSucursalAsync(idServicio, idSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException(
                    $"Asociación entre Servicio {idServicio} y Sucursal {idSucursal} no encontrada");

            servicioSucursal.Precio = nuevoPrecio;
            await _servicioSucursalRepository.UpdateAsync(servicioSucursal);

            return _mapper.Map<ServicioSucursalResponseDto>(servicioSucursal);
        }

        /// <summary>
        /// Obtiene servicios-sucursales dentro de un rango de precio
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> GetByRangoPrecioAsync(decimal precioMin, decimal precioMax)
        {
            if (precioMin < 0 || precioMax < 0 || precioMin > precioMax)
                throw new InvalidOperationException("El rango de precios no es válido");

            var servicios = await _servicioSucursalRepository.GetByRangoPrecioAsync(precioMin, precioMax);
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(servicios);
        }

        /// <summary>
        /// Obtiene el precio promedio de un servicio en todas las sucursales
        /// </summary>
        public async Task<decimal> GetPrecioPromedioPorServicioAsync(int idServicio)
        {
            var servicio = await _servicioRepository.GetByIdAsync(idServicio);
            if (servicio == null)
                throw new KeyNotFoundException($"Servicio con ID {idServicio} no encontrado");

            return await _servicioSucursalRepository.GetPrecioPromedioPorServicioAsync(idServicio);
        }

        /// <summary>
        /// Alterna el estado de un servicio en una sucursal
        /// </summary>
        public async Task<IEnumerable<ServicioSucursalResponseDto>> ToggleEstadoAsync(int idServicio, int idSucursal)
        {
            var servicioSucursal = await _servicioSucursalRepository.GetByServicioAndSucursalAsync(idServicio, idSucursal);
            if (servicioSucursal == null)
                throw new KeyNotFoundException(
                    $"Asociación entre Servicio {idServicio} y Sucursal {idSucursal} no encontrada");

            servicioSucursal.Estado = !servicioSucursal.Estado;
            await _servicioSucursalRepository.UpdateAsync(servicioSucursal);

            // Retornar todos los servicios de la sucursal para confirmación
            var servicios = await _servicioSucursalRepository.GetBySucursalAsync(idSucursal);
            return _mapper.Map<IEnumerable<ServicioSucursalResponseDto>>(servicios);
        }
    }
}
