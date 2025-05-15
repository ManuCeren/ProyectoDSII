using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Conductore
{
    public int IdConductores { get; set; }

    public string? Nombre { get; set; }

    public string? Licencia { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public string? Estado { get; set; }

    public string? Telefono { get; set; }

    public int? IdVehiculo { get; set; }

    public virtual Unidade? IdVehiculoNavigation { get; set; }
}
