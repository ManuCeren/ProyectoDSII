using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;    

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly TransporteFloresDbContext dbContext;
        public ClienteController(TransporteFloresDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Get()
        {
            var listaCliente = await dbContext.Clientes.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaCliente);
        }
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.IdClientes == id);
            return StatusCode(StatusCodes.Status200OK, cliente);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Cliente objeto)
        {
            await dbContext.Clientes.AddAsync(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Cliente objeto)
        {
            dbContext.Clientes.Update(objeto);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.IdClientes == id);
            dbContext.Clientes.Remove(cliente);
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
