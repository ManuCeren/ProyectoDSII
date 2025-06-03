using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturacionController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public FacturacionController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        // Obtener lista de todas las facturas
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaFacturacion = await dbContext.Facturacions
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdEnvioNavigation)
                .ToListAsync();

            return Ok(listaFacturacion);
        }

        // Obtener una factura específica por su ID
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var facturacion = await dbContext.Facturacions
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdEnvioNavigation)
                .FirstOrDefaultAsync(f => f.IdFacturacion == id);

            if (facturacion == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            return Ok(facturacion);
        }

        // Crear una nueva factura
        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Facturacion facturacion)
        {
            if (facturacion == null)
            {
                return BadRequest(new { mensaje = "La factura no puede ser nula" });
            }

            await dbContext.Facturacions.AddAsync(facturacion);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = facturacion.IdFacturacion }, facturacion); // Retorna 201 Created
        }

        // Editar una factura existente
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Facturacion facturacion)
        {
            if (facturacion == null)
            {
                return BadRequest(new { mensaje = "La factura no puede ser nula" });
            }

            var facturaExistente = await dbContext.Facturacions
                .FirstOrDefaultAsync(f => f.IdFacturacion == facturacion.IdFacturacion);

            if (facturaExistente == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            dbContext.Facturacions.Update(facturacion);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Factura actualizada con éxito" });
        }

        // Eliminar una factura por su ID
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var facturacion = await dbContext.Facturacions
                .FirstOrDefaultAsync(f => f.IdFacturacion == id);

            if (facturacion == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            dbContext.Facturacions.Remove(facturacion);
            await dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Factura eliminada con éxito" });
        }
    }
}
