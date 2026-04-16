namespace Aplicacion_ReservasStyle.DTOs
{
    public class HorarioLocalResponseDto
    {
        public int IdHorarioLocal { get; set; }
        public int IdSucursal { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraAbierto { get; set; }
        public TimeSpan HoraCerrado { get; set; }
        public bool Estado { get; set; }
    }
}
