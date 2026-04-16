using Dominio_ReservasStyle.Enums;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class HorariosDisponiblesResponseDto
    {
        public int IdHorario { get; set; }
        public int IdEmpleado { get; set; }
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string DiaSemanaTexto => DiaSemana.ToString();
    }
}
