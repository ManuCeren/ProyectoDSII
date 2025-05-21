using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public string? NombreCliente { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? TipoCliente { get; set; }

    public virtual ICollection<Envio> Envíos { get; set; } = new List<Envio>();

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();
}
