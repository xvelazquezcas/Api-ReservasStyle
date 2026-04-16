using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class PromocionesService : IPromocionesService
    {
        private readonly IPromocionesRepository _promocionesRepository;
        private readonly IMapper _mapper;

        public PromocionesService(IPromocionesRepository promocionesRepository, IMapper mapper)
        {
            _promocionesRepository = promocionesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las promociones
        /// </summary>
        public async Task<IEnumerable<PromocionesResponseDto>> GetAllAsync()
        {
            var promociones = await _promocionesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PromocionesResponseDto>>(promociones);
        }

        /// <summary>
        /// Obtiene una promoción por ID
        /// </summary>
        public async Task<PromocionesResponseDto> GetByIdAsync(int id)
        {
            var promocion = await _promocionesRepository.GetByIdAsync(id);
            if (promocion == null)
                throw new KeyNotFoundException($"Promoción con ID {id} no encontrada");

            return _mapper.Map<PromocionesResponseDto>(promocion);
        }

        /// <summary>
        /// Crea una nueva promoción
        /// </summary>
        public async Task<PromocionesResponseDto> CreateAsync(CrearPromocionesDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            var existeNombre = await _promocionesRepository.ExisteNombreAsync(dto.Nombre);
            if (existeNombre)
                throw new InvalidOperationException(
                    $"Ya existe una promoción con el nombre '{dto.Nombre}'");

            // ✅ Validar que FechaFin > FechaInicio
            if (dto.FechaFin <= dto.FechaInicio)
                throw new InvalidOperationException(
                    "La FechaFin debe ser mayor que la FechaInicio");

            // ✅ Validar porcentaje válido
            if (dto.PorcentajeDescuento <= 0 || dto.PorcentajeDescuento > 100)
                throw new InvalidOperationException(
                    "El PorcentajeDescuento debe estar entre 0.01 y 100");

            // ✅ MAPEO DTO → ENTIDAD
            var promocion = _mapper.Map<Promociones>(dto);

            // ✅ PERSISTENCIA
            await _promocionesRepository.CreateAsync(promocion);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<PromocionesResponseDto>(promocion);
        }

        /// <summary>
        /// Actualiza una promoción existente
        /// </summary>
        public async Task<PromocionesResponseDto> UpdateAsync(int id, ActualizarPromocionesDto dto)
        {
            var promocion = await _promocionesRepository.GetByIdAsync(id);
            if (promocion == null)
                throw new KeyNotFoundException($"Promoción con ID {id} no encontrada");

            // ✅ VALIDAR SI EL NOMBRE CAMBIÓ
            if (promocion.Nombre != dto.Nombre)
            {
                var existeNombre = await _promocionesRepository.ExisteNombreAsync(dto.Nombre);
                if (existeNombre)
                    throw new InvalidOperationException(
                        $"Ya existe otra promoción con el nombre '{dto.Nombre}'");
            }

            // ✅ Validar que FechaFin > FechaInicio
            if (dto.FechaFin <= dto.FechaInicio)
                throw new InvalidOperationException(
                    "La FechaFin debe ser mayor que la FechaInicio");

            // ✅ Validar porcentaje válido
            if (dto.PorcentajeDescuento <= 0 || dto.PorcentajeDescuento > 100)
                throw new InvalidOperationException(
                    "El PorcentajeDescuento debe estar entre 0.01 y 100");

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, promocion);

            // ✅ PERSISTENCIA
            await _promocionesRepository.UpdateAsync(promocion);

            return _mapper.Map<PromocionesResponseDto>(promocion);
        }

        /// <summary>
        /// Elimina una promoción
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var promocion = await _promocionesRepository.GetByIdAsync(id);
            if (promocion == null)
                throw new KeyNotFoundException($"Promoción con ID {id} no encontrada");

            await _promocionesRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene promociones activas
        /// </summary>
        public async Task<IEnumerable<PromocionesResponseDto>> GetActivasAsync()
        {
            var promociones = await _promocionesRepository.GetActivasAsync();
            return _mapper.Map<IEnumerable<PromocionesResponseDto>>(promociones);
        }

        /// <summary>
        /// Obtiene promociones vigentes (dentro del rango de fechas)
        /// </summary>
        public async Task<IEnumerable<PromocionesResponseDto>> GetVigentesAsync()
        {
            var promociones = await _promocionesRepository.GetVigentesAsync();
            return _mapper.Map<IEnumerable<PromocionesResponseDto>>(promociones);
        }

        /// <summary>
        /// Obtiene promociones próximas a comenzar
        /// </summary>
        public async Task<IEnumerable<PromocionesResponseDto>> GetProximasAsync(int dias = 7)
        {
            if (dias < 1)
                throw new InvalidOperationException("Los días deben ser mayor a 0");

            var promociones = await _promocionesRepository.GetProximasAsync(dias);
            return _mapper.Map<IEnumerable<PromocionesResponseDto>>(promociones);
        }

        /// <summary>
        /// Verifica si una promoción está vigente
        /// </summary>
        public async Task<bool> IsPromocionVigenteAsync(int idPromocion)
        {
            return await _promocionesRepository.IsPromocionVigenteAsync(idPromocion);
        }
    }
}
