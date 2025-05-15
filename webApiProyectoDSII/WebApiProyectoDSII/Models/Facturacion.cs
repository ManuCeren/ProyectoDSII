using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Facturacion
{
    public int IdFacturacion { get; set; }

    public int? IdCliente { get; set; }

    public DateOnly? FechaFactura { get; set; }

    public decimal? MontoTotal { get; set; }

    public string? EstadoPago { get; set; }

    public int? IdEnvio { get; set; }

    public virtual ICollection<DetalleFacturacion> DetalleFacturacions { get; set; } = new List<DetalleFacturacion>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Envío? IdEnvioNavigation { get; set; }
}
