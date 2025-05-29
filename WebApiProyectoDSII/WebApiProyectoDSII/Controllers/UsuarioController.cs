using BCrypt.Net; // Importar la librería para bcrypt
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TransporteFloresDbContext _dbContext;

        public UsuarioController(TransporteFloresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Usuario/Lista
        [HttpGet("Lista")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _dbContext.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/Obtener/{id}
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuarios == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return Ok(usuario);
        }

        // POST: api/Usuario/Nuevo
        [HttpPost("Nuevo")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null)
            {
                return BadRequest("Datos inválidos.");
            }

            // Encriptar la contraseña antes de guardarla en la base de datos
            nuevoUsuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.Contraseña);

            // Guardar el nuevo usuario en la base de datos
            await _dbContext.Usuarios.AddAsync(nuevoUsuario);
            await _dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario creado correctamente." });
        }

        // PUT: api/Usuario/Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> EditarUsuario([FromBody] Usuario usuarioEditado)
        {
            if (usuarioEditado == null || usuarioEditado.IdUsuarios == 0)
            {
                return BadRequest("Datos inválidos.");
            }

            var usuarioExistente = await _dbContext.Usuarios.FindAsync(usuarioEditado.IdUsuarios);

            if (usuarioExistente == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Encriptar la contraseña si ha sido modificada
            if (!string.IsNullOrEmpty(usuarioEditado.Contraseña))
            {
                usuarioExistente.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioEditado.Contraseña);
            }

            // Actualizar los campos del usuario
            usuarioExistente.NombreUsuario = usuarioEditado.NombreUsuario;
            usuarioExistente.Rol = usuarioEditado.Rol;
            usuarioExistente.Email = usuarioEditado.Email;

            _dbContext.Usuarios.Update(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario actualizado correctamente." });
        }

        // DELETE: api/Usuario/Eliminar/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuarios == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario eliminado correctamente." });
        }

        // POST: api/Usuario/IniciarSesion
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Datos inválidos.");
            }

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Verificar si la contraseña ingresada coincide con la encriptada
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Contraseña, usuario.Contraseña))
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            return Ok(new { mensaje = "Inicio de sesión exitoso" });
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}
