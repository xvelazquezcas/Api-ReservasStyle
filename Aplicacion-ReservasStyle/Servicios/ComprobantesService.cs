using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class ComprobantesService : IComprobantesService
    {
        private readonly IComprobantesRepository _comprobantesRepository;
        private readonly IMapper _mapper;

        public ComprobantesService(IComprobantesRepository comprobantesRepository, IMapper mapper)
        {
            _comprobantesRepository = comprobantesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los comprobantes
        /// </summary>
        public async Task<IEnumerable<ComprobantesResponseDto>> GetAllAsync()
        {
            var comprobantes = await _comprobantesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ComprobantesResponseDto>>(comprobantes);
        }

        /// <summary>
        /// Obtiene un comprobante por ID
        /// </summary>
        public async Task<ComprobantesResponseDto> GetByIdAsync(int id)
        {
            var comprobante = await _comprobantesRepository.GetByIdAsync(id);
            if (comprobante == null)
                throw new KeyNotFoundException($"Comprobante con ID {id} no encontrado");

            return _mapper.Map<ComprobantesResponseDto>(comprobante);
        }

        /// <summary>
        /// Crea un nuevo comprobante
        /// </summary>
        public async Task<ComprobantesResponseDto> CreateAsync(CrearComprobantesDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            var existeFolio = await _comprobantesRepository.ExisteFolioAsync(dto.Folio);
            if (existeFolio)
                throw new InvalidOperationException(
                    $"Ya existe un comprobante con el folio '{dto.Folio}'");

            // ✅ MAPEO DTO → ENTIDAD
            var comprobante = _mapper.Map<Comprobantes>(dto);

            // ✅ PERSISTENCIA
            await _comprobantesRepository.CreateAsync(comprobante);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<ComprobantesResponseDto>(comprobante);
        }

        /// <summary>
        /// Actualiza un comprobante existente
        /// </summary>
        public async Task<ComprobantesResponseDto> UpdateAsync(int id, ActualizarComprobantesDto dto)
        {
            var comprobante = await _comprobantesRepository.GetByIdAsync(id);
            if (comprobante == null)
                throw new KeyNotFoundException($"Comprobante con ID {id} no encontrado");

            // ✅ VALIDAR SI EL FOLIO CAMBIÓ Y SI YA EXISTE
            if (comprobante.Folio != dto.Folio)
            {
                var existeFolio = await _comprobantesRepository.ExisteFolioAsync(dto.Folio);
                if (existeFolio)
                    throw new InvalidOperationException(
                        $"Ya existe otro comprobante con el folio '{dto.Folio}'");
            }

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, comprobante);

            // ✅ PERSISTENCIA
            await _comprobantesRepository.UpdateAsync(comprobante);

            return _mapper.Map<ComprobantesResponseDto>(comprobante);
        }

        /// <summary>
        /// Elimina un comprobante
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var comprobante = await _comprobantesRepository.GetByIdAsync(id);
            if (comprobante == null)
                throw new KeyNotFoundException($"Comprobante con ID {id} no encontrado");

            await _comprobantesRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene comprobantes por IdPago
        /// </summary>
        public async Task<IEnumerable<ComprobantesResponseDto>> GetByIdPagoAsync(int idPago)
        {
            var comprobantes = await _comprobantesRepository.GetByIdPagoAsync(idPago);
            return _mapper.Map<IEnumerable<ComprobantesResponseDto>>(comprobantes);
        }
    }
}
