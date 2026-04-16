using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class LogMappingProfile : Profile
    {
        public LogMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearLogDto, Log>();

            // Entidad → Response DTO
            CreateMap<Log, LogResponseDto>();
        }
    }
}
