using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccioneController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public DireccioneController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // GET: api/Direccione/Lista
        [HttpGet("Lista")]
        public async Task<IActionResult> Get()
        {
            var direcciones = await dbContext.Direcciones.ToListAsync();
            return Ok(direcciones); 
        }

        // GET: api/Direccione/Obtener/5
        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var direccion = await dbContext.Direcciones
                                           .FirstOrDefaultAsync(d => d.IdDirecciones == id);

            if (direccion == null)
                return NotFound(new { mensaje = "Dirección no encontrada." });

            return Ok(direccion); 
        }

        // POST: api/Direccione/Nuevo
        [HttpPost("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Direccione direccion)
        {
            if (direccion == null)
                return BadRequest(new { mensaje = "El objeto Direccione es inválido." });

            // Crear una nueva dirección
            await dbContext.Direcciones.AddAsync(direccion);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Dirección creada correctamente." });
        }

        // PUT: api/Direccione/Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] Direccione direccion)
        {
            if (direccion == null)
                return BadRequest(new { mensaje = "El objeto Direccione es inválido." });

            var direccionExistente = await dbContext.Direcciones
                                                   .FirstOrDefaultAsync(d => d.IdDirecciones == direccion.IdDirecciones);

            if (direccionExistente == null)
                return NotFound(new { mensaje = "Dirección no encontrada." });

            // Actualizar las propiedades de la dirección existente
            direccionExistente.Direccion1 = direccion.Direccion1;
            direccionExistente.Direccion2 = direccion.Direccion2;

            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Dirección actualizada correctamente." });
        }

        // DELETE: api/Direccione/Eliminar/5
        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var direccion = await dbContext.Direcciones
                                           .FirstOrDefaultAsync(d => d.IdDirecciones == id);

            if (direccion == null)
                return NotFound(new { mensaje = "Dirección no encontrada." });

            // Eliminar la dirección
            dbContext.Direcciones.Remove(direccion);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Dirección eliminada correctamente." });
        }
    }
}
