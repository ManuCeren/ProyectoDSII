using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProyectoDSII.Repositories;
using WebApiProyectoDSII.Services;

[ApiController]
[Route("api/[controller]")]
public class ProyeccionController : ControllerBase
{
    private readonly EnviosRepositories _enviosRepo;
    private readonly ProyecctionService _proyeccionService;

    public ProyeccionController(EnviosRepositories enviosRepo, ProyecctionService proyeccionService)
    {
        _enviosRepo = enviosRepo;
        _proyeccionService = proyeccionService;
    }

    [HttpGet("Viajes")]
    public async Task<IActionResult> GetProyeccionViajes([FromQuery] int mesesHistorial = 12, [FromQuery] int mesesProyeccion = 3)
    {
        var historico = await _enviosRepo.GetMonthlyEnviosData(mesesHistorial);
        var resultado = _proyeccionService.CalculateLeastSquaresProjection(historico, mesesProyeccion, "Envios");
        return Ok(resultado);
    }

    [HttpGet("Ingresos")]
    public async Task<IActionResult> GetProyeccionIngresos([FromQuery] int mesesHistorial = 12, [FromQuery] int mesesProyeccion = 3)
    {
        var historico = await _enviosRepo.GetMonthlyIngresosData(mesesHistorial);
        var resultado = _proyeccionService.CalculateLeastSquaresProjection(historico, mesesProyeccion, "Ingresos");
        return Ok(resultado);
    }
}

