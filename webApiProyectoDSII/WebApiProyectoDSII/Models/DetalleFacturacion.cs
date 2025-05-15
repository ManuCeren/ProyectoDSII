using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class DetalleFacturacion
{
    public int IdDetalleFacturacion { get; set; }

    public int? IdFacturacion { get; set; }

    public string? Detalle { get; set; }

    public decimal? Precio { get; set; }

    public virtual Facturacion? IdFacturacionNavigation { get; set; }
}
