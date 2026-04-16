using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repo;

        public ServicioService(IServicioRepository repo)
        {
            _repo = repo;
        }

        //GET ALL
        public async Task<IEnumerable<ServicioDto>> ObtenerTodos()
        {
            var lista = await _repo.GetAllAsync();

            return lista
                .Where(s => s.Estado)
                .Select(s => new ServicioDto
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    DuracionMinutos = s.DuracionMinutos,
                    Imagen = s.Imagen,
                    Estado = s.Estado
                });
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
        public async Task<ServicioDto> Crear(ServicioCreateDto dto)
        {
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

            //Id Generado
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

        //UPDATE
        public async Task Actualizar(ServicioUpdateDto dto)
        {
            var servicio = await _repo.GetByIdAsync(dto.IdServicio);

            if (servicio == null)
                throw new Exception("Servicio no encontrado");

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

        //DELETE
        public async Task<bool> Eliminar(int id)
        {
            var servicio = await _repo.GetByIdAsync(id);

            if (servicio == null)
                return false;

            await _repo.DeleteAsync(servicio);

            return true;
        }
    }
}