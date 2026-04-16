using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class ComprobantesMappingProfile : Profile
    {
        public ComprobantesMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearComprobantesDto, Comprobantes>();

            // Actualizar DTO → Entidad
            CreateMap<ActualizarComprobantesDto, Comprobantes>();

            // Entidad → Response DTO
            CreateMap<Comprobantes, ComprobantesResponseDto>();
        }
    }
}
