using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class HorariosDisponiblesMappingProfile : Profile
    {
        public HorariosDisponiblesMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearHorariosDisponiblesDto, HorariosDisponibles>();

            // Actualizar DTO → Entidad
            CreateMap<ActualizarHorariosDisponiblesDto, HorariosDisponibles>();

            // Entidad → Response DTO
            CreateMap<HorariosDisponibles, HorariosDisponiblesResponseDto>();
        }
    }
}
