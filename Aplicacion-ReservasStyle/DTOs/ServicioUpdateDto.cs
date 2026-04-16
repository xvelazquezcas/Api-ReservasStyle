using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class ServicioUpdateDto
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public string? Imagen { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
