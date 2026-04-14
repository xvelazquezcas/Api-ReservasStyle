using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using AutoMapper;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public EmpleadoService(IMapper mapper, IEmpleadoRepository empleadoRepository)
        {
            _mapper = mapper;
            _empleadoRepository = empleadoRepository;
        }

        public async Task<EmpleadoResponseDto> CreateAsync(CrearEmpleadoDto dto)
        {
            var existeUSusario = await _empleadoRepository.ExisteEmpleadoIdAsync(dto.IdUsuario);
            if (existeUSusario)
                throw new InvalidOperationException(
                    $"Ya existe el usuario con el id '{dto.IdUsuario}'");

            var empleado = _mapper.Map<Empleado>(dto);
            empleado.Estado = true;

            await _empleadoRepository.CreateAsync(empleado);

            return _mapper.Map<EmpleadoResponseDto>(empleado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(id);
            if (empleado == null)
                throw new KeyNotFoundException($"Empleado con ID {id} no encontrado");

            await _empleadoRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<EmpleadoResponseDto>> GetAllAsync()
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmpleadoResponseDto>>(empleados);
        }

        public async Task<EmpleadoResponseDto> GetByIdAsync(int id)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(id);
            if (empleado == null)
                throw new KeyNotFoundException($"Empleado con ID {id} no encontrado");

            return _mapper.Map<EmpleadoResponseDto>(empleado);
        }

        public async Task<EmpleadoResponseDto> UpdateAsync(int id, ActualizarEmpleadoDto dto)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(id);
            if (empleado == null)
                throw new KeyNotFoundException($"Empleado con ID {id} no encontrado");

            if (empleado.IdUsuario != dto.IdUsuario)
            {
                var existeOtroEmpleado = await _empleadoRepository.ExisteEmpleadoIdAsync(dto.IdUsuario);
                if (existeOtroEmpleado)
                    throw new InvalidOperationException(
                        $"Ya existe otro empleado asignado al usuario con ID '{dto.IdUsuario}'");
            }

            _mapper.Map(dto, empleado);

            await _empleadoRepository.UpdateAsync(empleado);

            return _mapper.Map<EmpleadoResponseDto>(empleado);
        }
    }
}