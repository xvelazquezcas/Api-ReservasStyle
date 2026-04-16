using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class NotificacionesMappingProfile : Profile
    {
        public NotificacionesMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearNotificacionesDto, Notificaciones>();

            // Actualizar DTO → Entidad
            CreateMap<ActualizarNotificacionesDto, Notificaciones>();

            // Entidad → Response DTO
            CreateMap<Notificaciones, NotificacionesResponseDto>();
        }
    }
}
