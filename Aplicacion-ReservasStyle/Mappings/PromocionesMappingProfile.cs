using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class PromocionesMappingProfile : Profile
    {
        public PromocionesMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearPromocionesDto, Promociones>()
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => true));

            // Actualizar DTO → Entidad
            CreateMap<ActualizarPromocionesDto, Promociones>();

            // Entidad → Response DTO
            CreateMap<Promociones, PromocionesResponseDto>();
        }
    }
}
