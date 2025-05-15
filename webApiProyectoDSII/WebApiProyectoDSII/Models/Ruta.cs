using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Ruta
{
    public int IdRutas { get; set; }

    public string? Origen { get; set; }

    public string? Destino { get; set; }

    public int? Distancia { get; set; }

    public virtual ICollection<Envío> Envíos { get; set; } = new List<Envío>();
}
