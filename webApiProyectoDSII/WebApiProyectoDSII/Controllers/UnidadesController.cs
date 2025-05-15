using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;

        public UnidadesController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaUnidades = await dbContext.Unidades.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaUnidades);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var unidad = await dbContext.Unidades.FirstOrDefaultAsync(u => u.IdUnidades == id);
            return StatusCode(StatusCodes.Status200OK, unidad);
        }

        [HttpPost]
        [Route("Nueva")]
        public async Task<IActionResult> Nueva([FromBody] Unidade objeto)
        {
            await dbContext.Unidades.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Unidade objeto)
        {
            dbContext.Unidades.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var unidad = await dbContext.Unidades.FirstOrDefaultAsync(u => u.IdUnidades == id);
            if (unidad == null)
            {
                return NotFound(new { mensaje = "Unidad no encontrada" });
            }

            dbContext.Unidades.Remove(unidad);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
