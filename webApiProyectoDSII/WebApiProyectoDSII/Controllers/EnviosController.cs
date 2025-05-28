using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;
        public EnvioController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaEnvios = await dbContext.Envios.ToListAsync();
            
            return StatusCode(StatusCodes.Status200OK, listaEnvios);
        }
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var envio = await dbContext.Envios.FirstOrDefaultAsync(c => c.IdEnvios == id);
            return StatusCode(StatusCodes.Status200OK, envio);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Envio objeto)
        {
            await dbContext.Envios.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Envio objeto)
        {
            dbContext.Envios.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var envio = await dbContext.Envios.FirstOrDefaultAsync(c => c.IdEnvios == id);
            dbContext.Envios.Remove(envio);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
        [HttpGet("VistaDetallada")]
        public async Task<IActionResult> ObtenerVistaDetallada()
        {
            var vista = await dbContext.VistaEnviosDetallados.ToListAsync();
            return Ok(vista);
        }
        [HttpGet("ResumenViajesPorCliente")]
        public async Task<IActionResult> GetViajesPorCliente()
        {
            var resumen = await dbContext.Envios
                .Join(dbContext.Clientes,
                      e => e.IdCliente,
                      c => c.IdClientes,
                      (e, c) => new { c.NombreCliente })
                .GroupBy(x => x.NombreCliente)
                .Select(g => new
                {
                    nombreCliente = g.Key,
                    cantidadViajes = g.Count()
                })
                .ToListAsync();

            return Ok(resumen);
        }

    }
}
