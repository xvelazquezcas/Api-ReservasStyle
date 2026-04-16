using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class NotificacionesService : INotificacionesService
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly IMapper _mapper;

        public NotificacionesService(INotificacionesRepository notificacionesRepository, IMapper mapper)
        {
            _notificacionesRepository = notificacionesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las notificaciones
        /// </summary>
        public async Task<IEnumerable<NotificacionesResponseDto>> GetAllAsync()
        {
            var notificaciones = await _notificacionesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificacionesResponseDto>>(notificaciones);
        }

        /// <summary>
        /// Obtiene una notificación por ID
        /// </summary>
        public async Task<NotificacionesResponseDto> GetByIdAsync(int id)
        {
            var notificacion = await _notificacionesRepository.GetByIdAsync(id);
            if (notificacion == null)
                throw new KeyNotFoundException($"Notificación con ID {id} no encontrada");

            return _mapper.Map<NotificacionesResponseDto>(notificacion);
        }

        /// <summary>
        /// Crea una nueva notificación
        /// </summary>
        public async Task<NotificacionesResponseDto> CreateAsync(CrearNotificacionesDto dto)
        {
            // ✅ VALIDACIONES DE NEGOCIO
            // Aquí puedes agregar validaciones adicionales si es necesario
            // Por ejemplo: verificar que el usuario exista

            // ✅ MAPEO DTO → ENTIDAD
            var notificacion = _mapper.Map<Notificaciones>(dto);
            notificacion.Leida = false; // Por defecto no leída

            // ✅ PERSISTENCIA
            await _notificacionesRepository.CreateAsync(notificacion);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<NotificacionesResponseDto>(notificacion);
        }

        /// <summary>
        /// Actualiza una notificación existente
        /// </summary>
        public async Task<NotificacionesResponseDto> UpdateAsync(int id, ActualizarNotificacionesDto dto)
        {
            var notificacion = await _notificacionesRepository.GetByIdAsync(id);
            if (notificacion == null)
                throw new KeyNotFoundException($"Notificación con ID {id} no encontrada");

            // ✅ ACTUALIZAR PROPIEDADES
            _mapper.Map(dto, notificacion);

            // ✅ PERSISTENCIA
            await _notificacionesRepository.UpdateAsync(notificacion);

            return _mapper.Map<NotificacionesResponseDto>(notificacion);
        }

        /// <summary>
        /// Elimina una notificación
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var notificacion = await _notificacionesRepository.GetByIdAsync(id);
            if (notificacion == null)
                throw new KeyNotFoundException($"Notificación con ID {id} no encontrada");

            await _notificacionesRepository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// Obtiene notificaciones por usuario
        /// </summary>
        public async Task<IEnumerable<NotificacionesResponseDto>> GetByIdUsuarioAsync(int idUsuario)
        {
            var notificaciones = await _notificacionesRepository.GetByIdUsuarioAsync(idUsuario);
            return _mapper.Map<IEnumerable<NotificacionesResponseDto>>(notificaciones);
        }

        /// <summary>
        /// Obtiene todas las notificaciones no leídas del sistema
        /// </summary>
        public async Task<IEnumerable<NotificacionesResponseDto>> GetNoLeidasAsync()
        {
            var notificaciones = await _notificacionesRepository.GetNoLeidasAsync();
            return _mapper.Map<IEnumerable<NotificacionesResponseDto>>(notificaciones);
        }

        /// <summary>
        /// Obtiene notificaciones no leídas de un usuario específico
        /// </summary>
        public async Task<IEnumerable<NotificacionesResponseDto>> GetNoLeidasByUsuarioAsync(int idUsuario)
        {
            var notificaciones = await _notificacionesRepository.GetNoLeidasByUsuarioAsync(idUsuario);
            return _mapper.Map<IEnumerable<NotificacionesResponseDto>>(notificaciones);
        }

        /// <summary>
        /// Marca una notificación como leída
        /// </summary>
        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notificacion = await _notificacionesRepository.GetByIdAsync(id);
            if (notificacion == null)
                throw new KeyNotFoundException($"Notificación con ID {id} no encontrada");

            await _notificacionesRepository.MarkAsReadAsync(id);
            return true;
        }

        /// <summary>
        /// Marca todas las notificaciones de un usuario como leídas
        /// </summary>
        public async Task<bool> MarkAllAsReadByUsuarioAsync(int idUsuario)
        {
            await _notificacionesRepository.MarkAllAsReadByUsuarioAsync(idUsuario);
            return true;
        }
    }
}
