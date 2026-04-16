using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Mappings
{
    public class HorarioLocalMappingProfile : Profile
    {
        public HorarioLocalMappingProfile()
        {
            // Crear DTO → Entidad
            CreateMap<CrearHorarioLocalDto, HorarioLocal>();

            // Actualizar DTO → Entidad
            CreateMap<ActualizarHorarioLocalDto, HorarioLocal>();

            // Entidad → Response DTO
            CreateMap<HorarioLocal, HorarioLocalResponseDto>();
        }
    }
}
