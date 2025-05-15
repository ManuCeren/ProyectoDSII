using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Mantenimiento
{
    public int IdMantenimientos { get; set; }

    public int? IdUnidad { get; set; }

    public DateOnly? FechaMantenimiento { get; set; }

    public DateOnly? FechaSiguienteMantenimiento { get; set; }

    public virtual Unidade? IdUnidadNavigation { get; set; }
}
