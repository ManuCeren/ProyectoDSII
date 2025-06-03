using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Ruta
{
    public int IdRutas { get; set; }

    public string? Origen { get; set; }

    public string? Destino { get; set; }

    public double? Distancia { get; set; }

    public virtual ICollection<Envio> Envíos { get; set; } = new List<Envio>();
}
