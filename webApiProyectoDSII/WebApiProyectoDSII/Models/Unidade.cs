using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Unidade
{
    public int IdUnidades { get; set; }

    public string? Placa { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? Año { get; set; }

    public string? Estado { get; set; }

    public int? KilometrajeActual { get; set; }

    public virtual ICollection<Conductore> Conductores { get; set; } = new List<Conductore>();

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
