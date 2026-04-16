using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class HorarioLocalService : IHorarioLocalService
    {
        private readonly IHorarioLocalRepository _horarioLocalRepository;
        private readonly IMapper _mapper;

        public HorarioLocalService(IHorarioLocalRepository horarioLocalRepository, IMapper mapper)
        {
            _horarioLocalRepository = horarioLocalRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los horarios locales
        /// </summary>
        public async Task<IEnumerable<HorarioLocalResponseDto>> GetAllAsync()
        {
            var horariosLocales = await _horarioLocalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HorarioLocalResponseDto>>(horariosLocales);
        }

        /// <summary>
        /// Obtiene un horario local por ID
        /// </summary>
        public async Task<HorarioLocalResponseDto> GetByIdAsync(int id)
        {
            var horarioLocal = await _horarioLocalRepository.GetByIdAsync(id);
            if (horarioLocal == null)
                throw new KeyNotFoundException($"Horario Local con ID {id} no encontrado");

            return _mapper.Map<HorarioLocalResponseDto>(horarioLocal);
        }

        /// <summary>
        /// Crea un nuevo horario local
        /// </summary>
        public async Task<HorarioLocalResponseDto> CreateAsync(CrearHorarioLocalDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            if (dto.HoraCerrado <= dto.HoraAbierto)
                throw new InvalidOperationException(
                    "La hora de cierre debe ser mayor que la hora de apertura");

            var yaExiste = await _horarioLocalRepository.GetByIdSucursalAndDiaAsync(dto.IdSucursal, dto.DiaSemana);
            if (yaExiste != null)
                throw new InvalidOperationException(
                    $"Ya existe un horario para la sucursal {dto.IdSucursal} en el día '{dto.DiaSemana}'");

            // ✅ MAPEO DTO → ENTIDAD
            var horarioLocal = _mapper.Map<HorarioLocal>(dto);

            // ✅ PERSISTENCIA
            await _horarioLocalRepository.CreateAsync(horarioLocal);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<HorarioLocalResponseDto>(horarioLocal);
        }

        /// <summary>
        /// Actualiza un horario local existente
        /// </summary>
        public async Task<HorarioLocalResponseDto> UpdateAsync(int id, ActualizarHorarioLocalDto dto)
        {
            var horarioLocal = await _horarioLocalRepository.GetByIdAsync(id);
            if (horarioLocal == null)
                throw new KeyNotFoundException($"Horario Local con ID {id} no encontrado");

            // ✅ VALIDAR HORAS
            if (dto.HoraCerrado <= dto.HoraAbierto)
                throw new InvalidOperationException(
                    "La hora de cierre debe ser mayor que la hora de apertura");

            // ✅ VALIDAR SI EL DÍA CAMBIÓ Y SI YA EXISTE
            if (horarioLocal.DiaSemana != dto.DiaSemana || horarioLocal.IdSucursal != dto.IdSucursal)
            {
                var yaExiste = await _horarioLocalRepository.GetByIdSucursalAndDiaAsync(dto.IdSucursal, dto.DiaSemana);
                if (yaExiste != null && yaExiste.IdHorarioLocal != id)
                    throw new InvalidOperationException(
                        $"Ya existe otro horario para la sucursal {dto.IdSucursal} en el día '{dto.DiaSemana}'");
            }

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, horarioLocal);

            // ✅ PERSISTENCIA
            await _horarioLocalRepository.UpdateAsync(horarioLocal);

            return _mapper.Map<HorarioLocalResponseDto>(horarioLocal);
        }

        /// <summary>
        /// Elimina un horario local
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var horarioLocal = await _horarioLocalRepository.GetByIdAsync(id);
            if (horarioLocal == null)
                throw new KeyNotFoundException($"Horario Local con ID {id} no encontrado");

            await _horarioLocalRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene horarios locales por IdSucursal
        /// </summary>
        public async Task<IEnumerable<HorarioLocalResponseDto>> GetByIdSucursalAsync(int idSucursal)
        {
            var horariosLocales = await _horarioLocalRepository.GetByIdSucursalAsync(idSucursal);
            return _mapper.Map<IEnumerable<HorarioLocalResponseDto>>(horariosLocales);
        }

        /// <summary>
        /// Obtiene un horario local por IdSucursal y DiaSemana
        /// </summary>
        public async Task<HorarioLocalResponseDto?> GetByIdSucursalAndDiaAsync(int idSucursal, string dia)
        {
            var horarioLocal = await _horarioLocalRepository.GetByIdSucursalAndDiaAsync(idSucursal, dia);
            if (horarioLocal == null)
                return null;

            return _mapper.Map<HorarioLocalResponseDto>(horarioLocal);
        }
    }
}
