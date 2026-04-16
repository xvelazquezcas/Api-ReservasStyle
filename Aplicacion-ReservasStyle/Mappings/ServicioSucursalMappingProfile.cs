using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class ServicioSucursalMappingProfile : Profile
    {
        public ServicioSucursalMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearServicioSucursalDto, ServicioSucursal>()
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => true));

            // Actualizar DTO → Entidad
            CreateMap<ActualizarServicioSucursalDto, ServicioSucursal>();

            // Entidad → Response DTO
            CreateMap<ServicioSucursal, ServicioSucursalResponseDto>();

            // Entidad → Detalle DTO
            CreateMap<ServicioSucursal, ServicioSucursalDetalleDto>()
                .ForMember(d => d.NombreServicio, opt => opt.MapFrom(s => s.Servicio!.Nombre))
                .ForMember(d => d.DescripcionServicio, opt => opt.MapFrom(s => s.Servicio!.Descripcion))
                .ForMember(d => d.DuracionMinutos, opt => opt.MapFrom(s => s.Servicio!.DuracionMinutos))
                .ForMember(d => d.NombreSucursal, opt => opt.MapFrom(s => s.Sucursal!.Nombre))
                .ForMember(d => d.UbicacionSucursal, opt => opt.MapFrom(s => s.Sucursal!.Ubicacion));
        }
    }
}
