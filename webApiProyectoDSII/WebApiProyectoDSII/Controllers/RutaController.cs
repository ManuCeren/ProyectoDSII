using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public RutaController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // GET: api/Ruta/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaRutas = await dbContext.Rutas.ToListAsync();
                if (listaRutas == null || listaRutas.Count == 0)
                {
                    return NotFound(new { mensaje = "No se encontraron rutas." });
                }
                return Ok(listaRutas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener las rutas", error = ex.Message });
            }
        }

        // GET: api/Ruta/Obtener/5
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ruta = await dbContext.Rutas.FirstOrDefaultAsync(r => r.IdRutas == id);
                if (ruta == null)
                {
                    return NotFound(new { mensaje = "Ruta no encontrada." });
                }
                return Ok(ruta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al obtener la ruta", error = ex.Message });
            }
        }

        // POST: api/Ruta/Nuevo
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Ruta objeto)
        {
            if (objeto == null)
            {
                return BadRequest(new { mensaje = "Datos de ruta inválidos." });
            }

            if (string.IsNullOrEmpty(objeto.Origen) || string.IsNullOrEmpty(objeto.Destino))
            {
                return BadRequest(new { mensaje = "Origen y destino son obligatorios." });
            }

            try
            {
                await dbContext.Rutas.AddAsync(objeto);
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Ruta creada correctamente.", rutaId = objeto.IdRutas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al crear la ruta", error = ex.Message });
            }
        }

        // PUT: api/Ruta/Editar
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Ruta objeto)
        {
            if (objeto == null || objeto.IdRutas <= 0)
            {
                return BadRequest(new { mensaje = "Datos de ruta inválidos." });
            }

            try
            {
                var rutaExistente = await dbContext.Rutas.FirstOrDefaultAsync(r => r.IdRutas == objeto.IdRutas);
                if (rutaExistente == null)
                {
                    return NotFound(new { mensaje = "Ruta no encontrada." });
                }

                // Actualizar propiedades
                rutaExistente.Origen = objeto.Origen;
                rutaExistente.Destino = objeto.Destino;
                rutaExistente.Distancia = objeto.Distancia;

                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Ruta actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar la ruta", error = ex.Message });
            }
        }

        // DELETE: api/Ruta/Eliminar/5
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { mensaje = "ID de ruta inválido." });
            }

            try
            {
                var ruta = await dbContext.Rutas.FirstOrDefaultAsync(r => r.IdRutas == id);
                if (ruta == null)
                {
                    return NotFound(new { mensaje = "Ruta no encontrada." });
                }

                dbContext.Rutas.Remove(ruta);
                await dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Ruta eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar la ruta", error = ex.Message });
            }
        }
    }
}
