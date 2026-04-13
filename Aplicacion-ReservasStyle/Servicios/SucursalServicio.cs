using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IMapper _mapper;

        public SucursalService(ISucursalRepository sucursalRepository, IMapper mapper)
        {
            _sucursalRepository = sucursalRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las sucursales activas
        /// </summary>
        public async Task<IEnumerable<SucursalResponseDto>> GetAllAsync()
        {
            var sucursales = await _sucursalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SucursalResponseDto>>(sucursales);
        }

        /// <summary>
        /// Obtiene una sucursal por ID
        /// </summary>
        public async Task<SucursalResponseDto> GetByIdAsync(int id)
        {
            var sucursal = await _sucursalRepository.GetByIdAsync(id);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {id} no encontrada");

            return _mapper.Map<SucursalResponseDto>(sucursal);
        }

        /// <summary>
        /// Crea una nueva sucursal
        /// </summary>
        public async Task<SucursalResponseDto> CreateAsync(CrearSucursalDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            var existeNombre = await _sucursalRepository.ExisteNombreAsync(dto.Nombre);
            if (existeNombre)
                throw new InvalidOperationException(
                    $"Ya existe una sucursal con el nombre '{dto.Nombre}'");

            // ✅ MAPEO DTO → ENTIDAD
            var sucursal = _mapper.Map<Sucursal>(dto);
            sucursal.EstadoActivo = true;

            // ✅ PERSISTENCIA
            await _sucursalRepository.CreateAsync(sucursal);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<SucursalResponseDto>(sucursal);
        }

        /// <summary>
        /// Actualiza una sucursal existente
        /// </summary>
        public async Task<SucursalResponseDto> UpdateAsync(int id, ActualizarSucursalDto dto)
        {
            var sucursal = await _sucursalRepository.GetByIdAsync(id);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {id} no encontrada");

            // ✅ VALIDAR SI EL NOMBRE CAMBIÓ Y SI YA EXISTE
            if (sucursal.Nombre != dto.Nombre)
            {
                var existeNombre = await _sucursalRepository.ExisteNombreAsync(dto.Nombre);
                if (existeNombre)
                    throw new InvalidOperationException(
                        $"Ya existe otra sucursal con el nombre '{dto.Nombre}'");
            }

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, sucursal);

            // ✅ PERSISTENCIA
            await _sucursalRepository.UpdateAsync(sucursal);

            return _mapper.Map<SucursalResponseDto>(sucursal);
        }

        /// <summary>
        /// Elimina una sucursal
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var sucursal = await _sucursalRepository.GetByIdAsync(id);
            if (sucursal == null)
                throw new KeyNotFoundException($"Sucursal con ID {id} no encontrada");

            await _sucursalRepository.DeleteAsync(id);
            return true;
        }
    }
}
