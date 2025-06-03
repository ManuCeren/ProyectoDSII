using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public ConductorController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public class EstadoDTO
        {
            public string Estado { get; set; }
        }
        // Obtener la lista de todos los conductores
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaConductores = await dbContext.Conductores.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaConductores);
        }

        // Obtener un conductor por su ID
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var conductor = await dbContext.Conductores.FirstOrDefaultAsync(c => c.IdConductores == id);
            if (conductor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Conductor no encontrado" });
            }
            return StatusCode(StatusCodes.Status200OK, conductor);
        }

        // Crear un nuevo conductor
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Conductore objeto)
        {
            // Validar que los datos estén completos
            if (objeto == null || string.IsNullOrEmpty(objeto.Nombre) || string.IsNullOrEmpty(objeto.Licencia))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Datos incompletos" });
            }

            // Guardar el nuevo conductor en la base de datos
            await dbContext.Conductores.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conductor creado correctamente" });
        }

        // Editar un conductor existente
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Conductore objeto)
        {
            // Verificar que el conductor exista
            var conductorExistente = await dbContext.Conductores.FirstOrDefaultAsync(c => c.IdConductores == objeto.IdConductores);
            if (conductorExistente == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Conductor no encontrado" });
            }

            // Actualizar los datos del conductor
            conductorExistente.Nombre = objeto.Nombre;
            conductorExistente.Licencia = objeto.Licencia;
            conductorExistente.FechaIngreso = objeto.FechaIngreso;
            conductorExistente.Estado = objeto.Estado;
            conductorExistente.Telefono = objeto.Telefono;
            conductorExistente.IdVehiculo = objeto.IdVehiculo;

            // Guardar los cambios en la base de datos
            dbContext.Conductores.Update(conductorExistente);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conductor actualizado correctamente" });
        }

        // Eliminar un conductor por su ID
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var conductor = await dbContext.Conductores.FirstOrDefaultAsync(c => c.IdConductores == id);
            if (conductor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Conductor no encontrado" });
            }

            // Eliminar el conductor de la base de datos
            dbContext.Conductores.Remove(conductor);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conductor eliminado correctamente" });
        }

        // Cambiar el estado de un conductor
        [HttpPatch]
        [Route("CambiarEstado/{id:int}")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoDTO dto)
        {
            var conductor = await dbContext.Conductores.FirstOrDefaultAsync(c => c.IdConductores == id);
            if (conductor == null)
            {
                return NotFound(new { mensaje = "Conductor no encontrado" });
            }

            conductor.Estado = dto.Estado;
            dbContext.Conductores.Update(conductor);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Estado actualizado correctamente" });
        }

    }
}
