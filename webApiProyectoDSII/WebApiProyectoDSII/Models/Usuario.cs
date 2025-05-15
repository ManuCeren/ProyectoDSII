using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Usuario
{
    public int IdUsuarios { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Rol { get; set; }

    public string? Contraseña { get; set; }

    public string? Email { get; set; }
}
