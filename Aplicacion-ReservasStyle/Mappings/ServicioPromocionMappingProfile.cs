using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class ServicioPromocionMappingProfile : Profile
    {
        public ServicioPromocionMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearServicioPromocionDto, ServicioPromocion>()
                .ForMember(d => d.FechaAsociacion, opt => opt.MapFrom(s => DateTime.Now));

            // Actualizar DTO → Entidad
            CreateMap<ActualizarServicioPromocionDto, ServicioPromocion>();

            // Entidad → Response DTO
            CreateMap<ServicioPromocion, ServicioPromocionResponseDto>();

            // Entidad → Detalle DTO (con información de la promoción)
            CreateMap<ServicioPromocion, ServicioPromocionDetalleDto>()
                .ForMember(d => d.NombrePromocion, opt => opt.MapFrom(s => s.Promocion!.Nombre))
                .ForMember(d => d.DescripcionPromocion, opt => opt.MapFrom(s => s.Promocion!.Descripcion))
                .ForMember(d => d.PorcentajeDescuento, opt => opt.MapFrom(s => s.Promocion!.PorcentajeDescuento))
                .ForMember(d => d.FechaInicioPromocion, opt => opt.MapFrom(s => s.Promocion!.FechaInicio))
                .ForMember(d => d.FechaFinPromocion, opt => opt.MapFrom(s => s.Promocion!.FechaFin))
                .ForMember(d => d.EstadoPromocion, opt => opt.MapFrom(s => s.Promocion!.Estado));
        }
    }
}
