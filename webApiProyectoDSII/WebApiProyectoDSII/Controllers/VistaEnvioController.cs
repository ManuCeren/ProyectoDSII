using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiProyectoDSII.Models;
using System.Linq;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaEnvioController : ControllerBase
    {
        private readonly TransporteFloresDbContext _dbContext;

        public VistaEnvioController(TransporteFloresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/VistaEnvio/Lista
        [HttpGet("Lista")]
        public async Task<IActionResult> GetLista()
        {
            var lista = await (from e in _dbContext.Envios
                               join c in _dbContext.Clientes on e.IdCliente equals c.IdClientes
                               join r in _dbContext.Rutas on e.IdRuta equals r.IdRutas
                               select new VistaEnvio
                               {
                                   IdEnvios = e.IdEnvios,
                                   FechaSolicitud = (DateOnly)e.FechaSolicitud,
                                   FechaEntregaEsperada = (DateOnly)e.FechaEntregaEsperada,
                                   IdCliente = (int)e.IdCliente,    // Agregado
                                   IdRuta = (int)e.IdRuta,          // Agregado
                                   Cliente = c.NombreCliente,
                                   Origen = r.Origen,
                                   Destino = r.Destino,
                                   Estado = e.Estado,
                                   Mercancia = e.Mercancia,
                                   Peso = (decimal)e.PesoTotal,
                                   Volumen = (decimal)e.VolumenTotal,
                                   costo = (decimal)e.CostoEnvio
                               }).ToListAsync();

            return Ok(lista);
        }

        // GET: api/VistaEnvio/Obtener/{id}
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var envio = await (from e in _dbContext.Envios
                               join c in _dbContext.Clientes on e.IdCliente equals c.IdClientes
                               join r in _dbContext.Rutas on e.IdRuta equals r.IdRutas
                               where e.IdEnvios == id
                               select new VistaEnvio
                               {
                                   IdEnvios = e.IdEnvios,
                                   FechaSolicitud = (DateOnly)e.FechaSolicitud,
                                   FechaEntregaEsperada = (DateOnly)e.FechaEntregaEsperada,
                                   IdCliente = (int)e.IdCliente,    // Agregado
                                   IdRuta = (int)e.IdRuta,          // Agregado
                                   Cliente = c.NombreCliente,
                                   Origen = r.Origen,
                                   Destino = r.Destino,
                                   Estado = e.Estado,
                                   Mercancia = e.Mercancia,
                                   Peso = (decimal)e.PesoTotal,
                                   Volumen = (decimal)e.VolumenTotal,
                                   costo = (decimal)e.CostoEnvio
                               }).FirstOrDefaultAsync();

            if (envio == null)
                return NotFound();

            return Ok(envio);
        }


        // POST: api/VistaEnvio/Nuevo
        [HttpPost("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Envio nuevoEnvio)
        {
            if (nuevoEnvio == null)
                return BadRequest("Datos inválidos.");

            await _dbContext.Envios.AddAsync(nuevoEnvio);
            await _dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Envío creado correctamente." });
        }

        // PUT: api/VistaEnvio/Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] Envio envioEditado)
        {
            if (envioEditado == null || envioEditado.IdEnvios == 0)
                return BadRequest("Datos inválidos.");

            var envioExistente = await _dbContext.Envios.FindAsync(envioEditado.IdEnvios);

            if (envioExistente == null)
                return NotFound("Envío no encontrado.");

            // Actualizar campos
            envioExistente.IdCliente = envioEditado.IdCliente;
            envioExistente.IdRuta = envioEditado.IdRuta;
            envioExistente.FechaSolicitud = envioEditado.FechaSolicitud;
            envioExistente.FechaEntregaEsperada = envioEditado.FechaEntregaEsperada;
            envioExistente.Estado = envioEditado.Estado;
            envioExistente.Mercancia = envioEditado.Mercancia;
            envioExistente.PesoTotal = envioEditado.PesoTotal;
            envioExistente.VolumenTotal = envioEditado.VolumenTotal;
            envioExistente.CostoEnvio = envioEditado.CostoEnvio;

            _dbContext.Envios.Update(envioExistente);
            await _dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Envío actualizado correctamente" });
        }
    }
}
