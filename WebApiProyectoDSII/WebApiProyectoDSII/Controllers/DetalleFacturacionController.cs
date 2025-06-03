using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturacionController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public DetalleFacturacionController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // GET: api/DetalleFacturacion/Lista
        [HttpGet("Lista")]
        public async Task<IActionResult> Get()
        {
            var detallesFacturacion = await dbContext.DetalleFacturacions
                                                      .Include(d => d.IdFacturacionNavigation)
                                                      .ToListAsync();
            return Ok(detallesFacturacion); // Devuelve la lista de todos los detalles de facturación
        }

        // GET: api/DetalleFacturacion/Obtener/5
        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var detalle = await dbContext.DetalleFacturacions
                                         .Include(d => d.IdFacturacionNavigation)
                                         .FirstOrDefaultAsync(d => d.IdDetalleFacturacion == id);

            if (detalle == null)
                return NotFound(new { mensaje = "Detalle de facturación no encontrado." });

            return Ok(detalle); // Devuelve el detalle de facturación solicitado
        }

        // POST: api/DetalleFacturacion/Nuevo
        [HttpPost("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] DetalleFacturacion detalleFacturacion)
        {
            if (detalleFacturacion == null)
                return BadRequest(new { mensaje = "El objeto DetalleFacturacion es inválido." });

            // Validar que la factura existe
            var facturaExistente = await dbContext.Facturacions.FindAsync(detalleFacturacion.IdFacturacion);
            if (facturaExistente == null)
                return BadRequest(new { mensaje = "La factura no existe." });

            // Crear el nuevo detalle de facturación
            await dbContext.DetalleFacturacions.AddAsync(detalleFacturacion);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Detalle de facturación creado correctamente." });
        }

        // PUT: api/DetalleFacturacion/Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] DetalleFacturacion detalleFacturacion)
        {
            if (detalleFacturacion == null)
                return BadRequest(new { mensaje = "El objeto DetalleFacturacion es inválido." });

            var detalleExistente = await dbContext.DetalleFacturacions
                                                  .FirstOrDefaultAsync(d => d.IdDetalleFacturacion == detalleFacturacion.IdDetalleFacturacion);

            if (detalleExistente == null)
                return NotFound(new { mensaje = "Detalle de facturación no encontrado." });

            // Actualizar propiedades del detalle de facturación
            detalleExistente.Detalle = detalleFacturacion.Detalle;
            detalleExistente.Precio = detalleFacturacion.Precio;
            detalleExistente.IdFacturacion = detalleFacturacion.IdFacturacion;

            await dbContext.SaveChangesAsync();
            return Ok(new { mensaje = "Detalle de facturación actualizado correctamente." });
        }

        // DELETE: api/DetalleFacturacion/Eliminar/5
        [HttpDelete("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var detalle = await dbContext.DetalleFacturacions
                                         .FirstOrDefaultAsync(d => d.IdDetalleFacturacion == id);
            if (detalle == null)
                return NotFound(new { mensaje = "Detalle de facturación no encontrado." });

            // Eliminar el detalle de facturación
            dbContext.DetalleFacturacions.Remove(detalle);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Detalle de facturación eliminado correctamente." });
        }
    }
}
