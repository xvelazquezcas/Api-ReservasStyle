using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class SucursalMappingProfile : Profile
    {
        public SucursalMappingProfile()
        {
            // DTO → Entidad
            CreateMap<CrearSucursalDto, Sucursal>()
                .ForMember(dest => dest.EstadoActivo, opt => opt.MapFrom(src => true));

            CreateMap<ActualizarSucursalDto, Sucursal>();

            // Entidad → DTO
            CreateMap<Sucursal, SucursalResponseDto>();
        }
    }
}
