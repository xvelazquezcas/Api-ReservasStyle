using Dominio_ReservasStyle.Enums;

namespace Dominio_ReservasStyle.Entities
{
    public class HorariosDisponibles //para empleados
    {
        public int IdHorario { get; set; }
        public int IdEmpleado { get; set; }
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

    }
}