using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IServicioService
    {
        Task<IEnumerable<ServicioDto>> ObtenerTodos();
        Task<ServicioDto?> ObtenerPorId(int id);
        Task<ServicioDto> Crear(ServicioCreateDto dto); 
        Task Actualizar(ServicioUpdateDto dto);
        Task<bool> Eliminar(int id);
    }
}
