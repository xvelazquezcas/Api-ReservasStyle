using Aplicacion_ReservasStyle.DTOs;
using AutoMapper;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class EmpleadoMappingProfile : Profile
    {
        public EmpleadoMappingProfile()
        {
            CreateMap<CrearEmpleadoDto, Empleado>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => true));

            CreateMap<ActualizarEmpleadoDto, Empleado>();

            // Entidad → DTO
            CreateMap<Empleado, EmpleadoResponseDto>();
        }
    }
}