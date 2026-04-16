using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class PagoMappingProfile : Profile
    {
        public PagoMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearPagoDto, Pago>()
                .ForMember(d => d.EstadoPago, opt => opt.MapFrom(s => "Pendiente"));

            // Actualizar DTO → Entidad
            CreateMap<ActualizarPagoDto, Pago>();

            // Entidad → Response DTO
            CreateMap<Pago, PagoResponseDto>();
        }
    }
}
