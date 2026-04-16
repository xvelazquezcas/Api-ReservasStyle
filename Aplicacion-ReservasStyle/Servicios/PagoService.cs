using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMapper _mapper;

        public PagoService(IPagoRepository pagoRepository, IMapper mapper)
        {
            _pagoRepository = pagoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los pagos
        /// </summary>
        public async Task<IEnumerable<PagoResponseDto>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PagoResponseDto>>(pagos);
        }

        /// <summary>
        /// Obtiene un pago por ID
        /// </summary>
        public async Task<PagoResponseDto> GetByIdAsync(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null)
                throw new KeyNotFoundException($"Pago con ID {id} no encontrado");

            return _mapper.Map<PagoResponseDto>(pago);
        }

        /// <summary>
        /// Crea un nuevo pago
        /// </summary>
        public async Task<PagoResponseDto> CreateAsync(CrearPagoDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            if (dto.Monto <= 0)
                throw new InvalidOperationException("El monto debe ser mayor a 0");

            // ✅ MAPEO DTO → ENTIDAD
            var pago = _mapper.Map<Pago>(dto);

            // ✅ PERSISTENCIA
            await _pagoRepository.CreateAsync(pago);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<PagoResponseDto>(pago);
        }

        /// <summary>
        /// Actualiza un pago existente
        /// </summary>
        public async Task<PagoResponseDto> UpdateAsync(int id, ActualizarPagoDto dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null)
                throw new KeyNotFoundException($"Pago con ID {id} no encontrado");

            // ✅ VALIDACIONES DE NEGOCIO
            if (dto.Monto <= 0)
                throw new InvalidOperationException("El monto debe ser mayor a 0");

            var estadosValidos = new[] { "Pendiente", "Completado", "Fallido" };
            if (!estadosValidos.Contains(dto.EstadoPago))
                throw new InvalidOperationException($"EstadoPago debe ser uno de: {string.Join(", ", estadosValidos)}");

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, pago);

            // ✅ PERSISTENCIA
            await _pagoRepository.UpdateAsync(pago);

            return _mapper.Map<PagoResponseDto>(pago);
        }

        /// <summary>
        /// Elimina un pago
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null)
                throw new KeyNotFoundException($"Pago con ID {id} no encontrado");

            await _pagoRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene pagos por cita
        /// </summary>
        public async Task<IEnumerable<PagoResponseDto>> GetByCitaAsync(int idCita)
        {
            var pagos = await _pagoRepository.GetByCitaAsync(idCita);
            return _mapper.Map<IEnumerable<PagoResponseDto>>(pagos);
        }

        /// <summary>
        /// Obtiene pagos por estado
        /// </summary>
        public async Task<IEnumerable<PagoResponseDto>> GetByEstadoPagoAsync(string estadoPago)
        {
            var estadosValidos = new[] { "Pendiente", "Completado", "Fallido" };
            if (!estadosValidos.Contains(estadoPago))
                throw new InvalidOperationException($"EstadoPago debe ser uno de: {string.Join(", ", estadosValidos)}");

            var pagos = await _pagoRepository.GetByEstadoPagoAsync(estadoPago);
            return _mapper.Map<IEnumerable<PagoResponseDto>>(pagos);
        }

        /// <summary>
        /// Calcula el total pagado por una cita
        /// </summary>
        public async Task<decimal> CalcularTotalPorCitaAsync(int idCita)
        {
            return await _pagoRepository.CalcularTotalPorCitaAsync(idCita);
        }

        /// <summary>
        /// Obtiene todos los pagos pendientes
        /// </summary>
        public async Task<IEnumerable<PagoResponseDto>> GetPagosPendientesAsync()
        {
            var pagos = await _pagoRepository.GetPagosPendientesAsync();
            return _mapper.Map<IEnumerable<PagoResponseDto>>(pagos);
        }
    }
}
