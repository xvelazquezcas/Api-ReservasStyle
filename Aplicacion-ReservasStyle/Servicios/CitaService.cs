using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IHorariosRepository _horariosRepository;

        public CitaService(ICitaRepository citaRepository, IHorariosRepository horariosRepository)
        {
            _citaRepository = citaRepository;
            _horariosRepository = horariosRepository;
        }

        public async Task<IEnumerable<Citas>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }

        public async Task<Citas?> GetByIdAsync(int id)
        {
            return await _citaRepository.GetByIdAsync(id);
        }

        public async Task<Citas> CreateAsync(Citas cita)
        {
            //Aqui ya usamos y validamos la hora de inicio y HoraFin tambien manejamos la fecha creacion y el estado
            
            // Validar rango de horas
            if (cita.HoraInicio >= cita.HoraFin)
                throw new Exception("La hora de inicio debe ser menor que la hora de fin");
            
            // VALIDAR HORARIO DEL EMPLEADO
            var dia = (DiaSemana)cita.Fecha.DayOfWeek;

            var horario = await _horariosRepository.GetHorarioAsync(cita.IdEmpleado, dia);

            if (horario == null)
                throw new Exception("El empleado no trabaja ese día");

            if (cita.HoraInicio < horario.HoraInicio || cita.HoraFin > horario.HoraFin)
                throw new Exception("La cita está fuera del horario del empleado");

            // Validar traslape de citas
            bool existe = await _citaRepository.ExisteCitaTraslapadaAsync(
                cita.IdEmpleado,
                cita.Fecha,
                cita.HoraInicio,
                cita.HoraFin
            );

            if (existe)
                throw new Exception("El empleado ya tiene una cita en ese horario");

            cita.FechaCreacion = DateTime.Now;
            cita.Estado = "Pendiente";

            return await _citaRepository.CreateAsync(cita);
        }

        public async Task UpdateAsync(Citas cita)
        {
            await _citaRepository.UpdateAsync(cita);
        }

        public async Task DeleteAsync(int id)
        {
            await _citaRepository.DeleteAsync(id);
        }
    }
}
