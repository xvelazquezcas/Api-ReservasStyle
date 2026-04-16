using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_ReservasStyle.Servicios
{
    namespace Aplicacion_ReservasStyle.Servicios
    {
        public class ServicioService
        {
            private readonly IServicioRepository _repo;

            public ServicioService(IServicioRepository repo)
            {
                _repo = repo;
            }

            // GET ALL
            public async Task<List<ServicioDto>> ObtenerTodos()
            {
                var lista = await _repo.GetAllAsync();

                return lista
                    .Where(s => s.Estado) //solo activos
                    .Select(s => new ServicioDto
                    {
                        IdServicio = s.IdServicio,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        DuracionMinutos = s.DuracionMinutos,
                        Imagen = s.Imagen,
                        Estado = s.Estado
                    }).ToList();
            }

            //GET BY ID
            public async Task<ServicioDto?> ObtenerPorId(int id)
            {
                var servicio = await _repo.GetByIdAsync(id);

                if (servicio == null || !servicio.Estado)
                    return null;

                return new ServicioDto
                {
                    IdServicio = servicio.IdServicio,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    DuracionMinutos = servicio.DuracionMinutos,
                    Imagen = servicio.Imagen,
                    Estado = servicio.Estado
                };
            }

            //CREATE
            public async Task Crear(ServicioCreateDto dto)
            {
                // VALIDACIONES
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new Exception("El nombre es obligatorio");

                if (dto.DuracionMinutos <= 0)
                    throw new Exception("La duración debe ser mayor a 0");

                var servicio = new Servicio
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    DuracionMinutos = dto.DuracionMinutos,
                    Imagen = dto.Imagen,
                    Estado = true,
                    FechaCreacion = DateTime.UtcNow
                };

                await _repo.CreateAsync(servicio);
            }

            // UPDATE
            public async Task Actualizar(ServicioUpdateDto dto)
            {
                var servicio = await _repo.GetByIdAsync(dto.IdServicio);

                if (servicio == null)
                    throw new Exception("Servicio no encontrado");

                // 🔥 VALIDACIONES
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new Exception("El nombre es obligatorio");

                if (dto.DuracionMinutos <= 0)
                    throw new Exception("La duración debe ser mayor a 0");

                servicio.Nombre = dto.Nombre;
                servicio.Descripcion = dto.Descripcion;
                servicio.DuracionMinutos = dto.DuracionMinutos;
                servicio.Imagen = dto.Imagen;
                servicio.Estado = dto.Estado;

                await _repo.UpdateAsync(servicio);
            }

            // ✅ DELETE (SOFT DELETE)
            public async Task Eliminar(int id)
            {
                var servicio = await _repo.GetByIdAsync(id);

                if (servicio == null)
                    throw new Exception("Servicio no encontrado");

                servicio.Estado = false;

                await _repo.UpdateAsync(servicio);
            }
        }
    }
}