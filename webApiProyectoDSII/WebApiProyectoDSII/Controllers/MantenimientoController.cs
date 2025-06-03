using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public MantenimientoController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaMantenimientos = await dbContext.Mantenimientos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaMantenimientos);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var mantenimiento = await dbContext.Mantenimientos.FirstOrDefaultAsync(m => m.IdMantenimientos == id);
            if (mantenimiento == null)
                return NotFound(new { mensaje = "Mantenimiento no encontrado" });

            return StatusCode(StatusCodes.Status200OK, mantenimiento);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Mantenimiento objeto)
        {
            await dbContext.Mantenimientos.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Mantenimiento objeto)
        {
            var existente = await dbContext.Mantenimientos.FirstOrDefaultAsync(m => m.IdMantenimientos == objeto.IdMantenimientos);
            if (existente == null)
                return NotFound(new { mensaje = "Mantenimiento no encontrado" });

            // Actualizar campos
            existente.IdUnidad = objeto.IdUnidad;
            existente.FechaMantenimiento = objeto.FechaMantenimiento;
            existente.FechaSiguienteMantenimiento = objeto.FechaSiguienteMantenimiento;

            dbContext.Mantenimientos.Update(existente);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mantenimiento = await dbContext.Mantenimientos.FirstOrDefaultAsync(m => m.IdMantenimientos == id);
            if (mantenimiento == null)
                return NotFound(new { mensaje = "Mantenimiento no encontrado." });

            dbContext.Mantenimientos.Remove(mantenimiento);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
