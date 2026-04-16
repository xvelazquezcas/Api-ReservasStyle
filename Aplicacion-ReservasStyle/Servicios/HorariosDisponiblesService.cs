using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class HorariosDisponiblesService : IHorariosDisponiblesService
    {
        private readonly IHorariosDisponiblesRepository _horariosDisponiblesRepository;
        private readonly IMapper _mapper;

        public HorariosDisponiblesService(IHorariosDisponiblesRepository horariosDisponiblesRepository, IMapper mapper)
        {
            _horariosDisponiblesRepository = horariosDisponiblesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los horarios disponibles
        /// </summary>
        public async Task<IEnumerable<HorariosDisponiblesResponseDto>> GetAllAsync()
        {
            var horarios = await _horariosDisponiblesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HorariosDisponiblesResponseDto>>(horarios);
        }

        /// <summary>
        /// Obtiene un horario disponible por ID
        /// </summary>
        public async Task<HorariosDisponiblesResponseDto> GetByIdAsync(int id)
        {
            var horario = await _horariosDisponiblesRepository.GetByIdAsync(id);
            if (horario == null)
                throw new KeyNotFoundException($"Horario disponible con ID {id} no encontrado");

            return _mapper.Map<HorariosDisponiblesResponseDto>(horario);
        }

        /// <summary>
        /// Crea un nuevo horario disponible
        /// </summary>
        public async Task<HorariosDisponiblesResponseDto> CreateAsync(CrearHorariosDisponiblesDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            var existeHorario = await _horariosDisponiblesRepository.ExisteHorarioAsync(dto.IdEmpleado, dto.DiaSemana);
            if (existeHorario)
                throw new InvalidOperationException(
                    $"Ya existe un horario disponible para el empleado en {dto.DiaSemana}");

            // ✅ Validar que HoraFin > HoraInicio
            if (dto.HoraFin <= dto.HoraInicio)
                throw new InvalidOperationException(
                    "La hora de fin debe ser mayor que la hora de inicio");

            // ✅ MAPEO DTO → ENTIDAD
            var horario = _mapper.Map<HorariosDisponibles>(dto);

            // ✅ PERSISTENCIA
            await _horariosDisponiblesRepository.CreateAsync(horario);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<HorariosDisponiblesResponseDto>(horario);
        }

        /// <summary>
        /// Actualiza un horario disponible existente
        /// </summary>
        public async Task<HorariosDisponiblesResponseDto> UpdateAsync(int id, ActualizarHorariosDisponiblesDto dto)
        {
            var horario = await _horariosDisponiblesRepository.GetByIdAsync(id);
            if (horario == null)
                throw new KeyNotFoundException($"Horario disponible con ID {id} no encontrado");

            // ✅ VALIDAR SI EL EMPLEADO O DÍA CAMBIÓ
            if (horario.IdEmpleado != dto.IdEmpleado || horario.DiaSemana != dto.DiaSemana)
            {
                var existeHorario = await _horariosDisponiblesRepository.ExisteHorarioAsync(dto.IdEmpleado, dto.DiaSemana);
                if (existeHorario)
                    throw new InvalidOperationException(
                        $"Ya existe otro horario disponible para ese empleado en {dto.DiaSemana}");
            }

            // ✅ Validar que HoraFin > HoraInicio
            if (dto.HoraFin <= dto.HoraInicio)
                throw new InvalidOperationException(
                    "La hora de fin debe ser mayor que la hora de inicio");

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, horario);

            // ✅ PERSISTENCIA
            await _horariosDisponiblesRepository.UpdateAsync(horario);

            return _mapper.Map<HorariosDisponiblesResponseDto>(horario);
        }

        /// <summary>
        /// Elimina un horario disponible
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var horario = await _horariosDisponiblesRepository.GetByIdAsync(id);
            if (horario == null)
                throw new KeyNotFoundException($"Horario disponible con ID {id} no encontrado");

            await _horariosDisponiblesRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene horarios disponibles por empleado
        /// </summary>
        public async Task<IEnumerable<HorariosDisponiblesResponseDto>> GetByEmpleadoAsync(int idEmpleado)
        {
            var horarios = await _horariosDisponiblesRepository.GetByEmpleadoAsync(idEmpleado);
            return _mapper.Map<IEnumerable<HorariosDisponiblesResponseDto>>(horarios);
        }

        /// <summary>
        /// Obtiene horarios disponibles por día de la semana
        /// </summary>
        public async Task<IEnumerable<HorariosDisponiblesResponseDto>> GetByDiaAsync(DiaSemana dia)
        {
            var horarios = await _horariosDisponiblesRepository.GetByDiaAsync(dia);
            return _mapper.Map<IEnumerable<HorariosDisponiblesResponseDto>>(horarios);
        }
    }
}
