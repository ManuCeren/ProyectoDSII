using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Models;
using System.Text.Json;

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
                var listaRutas = await dbContext.Rutas.OrderByDescending(r => r.IdRutas).ToListAsync();
                if (listaRutas == null || listaRutas.Count == 0)
                {
                    return Ok(new { datos = new List<Ruta>(), mensaje = "No se encontraron rutas." });
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

        // POST: api/Ruta/Nuevo - Adaptado para recibir JSON del frontend
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] JsonElement jsonData)
        {
            try
            {
                // Log básico para debug (puedes comentar estas líneas en producción)
                Console.WriteLine($"📥 JSON recibido: {jsonData}");

                // Extraer datos del JSON
                if (!jsonData.TryGetProperty("Origen", out var origenElement) ||
                    !jsonData.TryGetProperty("Destino", out var destinoElement) ||
                    !jsonData.TryGetProperty("Distancia", out var distanciaElement))
                {
                    return BadRequest(new { mensaje = "Se requieren las propiedades: Origen, Destino, Distancia" });
                }

                string origen = origenElement.GetString() ?? "";
                string destino = destinoElement.GetString() ?? "";

                if (!distanciaElement.TryGetDouble(out double distancia))
                {
                    return BadRequest(new { mensaje = "La distancia debe ser un número válido." });
                }

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(origen) || string.IsNullOrWhiteSpace(destino))
                {
                    return BadRequest(new { mensaje = "Origen y destino son obligatorios." });
                }

                if (distancia <= 0)
                {
                    return BadRequest(new { mensaje = "La distancia debe ser mayor que cero." });
                }

                // Crear objeto Ruta
                var nuevaRuta = new Ruta
                {
                    Origen = origen.Trim(),
                    Destino = destino.Trim(),
                    Distancia = Math.Round(distancia, 2)
                };

                // Guardar en base de datos
                await dbContext.Rutas.AddAsync(nuevaRuta);
                await dbContext.SaveChangesAsync();

                Console.WriteLine($"✅ Ruta guardada: ID {nuevaRuta.IdRutas}, {nuevaRuta.Origen} -> {nuevaRuta.Destino} ({nuevaRuta.Distancia} km)");

                return Ok(new { mensaje = "Ruta creada correctamente.", rutaId = nuevaRuta.IdRutas });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear ruta: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al crear la ruta", error = ex.Message });
            }
        }

        // POST alternativo que acepta objeto Ruta directamente (para compatibilidad)
        [HttpPost]
        [Route("NuevoRuta")]
        public async Task<IActionResult> NuevoRuta([FromBody] Ruta objeto)
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