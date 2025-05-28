using System;
using System.Collections.Generic;

namespace WebApiProyectoDSII.Models;
public class VistaEnvio
{
    public int IdEnvios { get; set; }
    public DateOnly FechaSolicitud { get; set; }
    public DateOnly FechaEntregaEsperada { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Origen { get; set; } = string.Empty;
    public string Destino { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Mercancia { get; set; } = string.Empty;
    public decimal Peso { get; set; }
    public decimal Volumen { get; set; }
    public decimal Costo { get; set; }
}
