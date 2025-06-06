﻿using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Envio
{
    public int IdEnvios { get; set; }

    public int? IdCliente { get; set; }

    public int? IdRuta { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaEntregaEsperada { get; set; }

    public string? Estado { get; set; }

    public string? Mercancia { get; set; }

    public decimal? PesoTotal { get; set; }

    public decimal? VolumenTotal { get; set; }
    public decimal? CostoEnvio { get; set; }
    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Ruta? IdRutaNavigation { get; set; }
}
