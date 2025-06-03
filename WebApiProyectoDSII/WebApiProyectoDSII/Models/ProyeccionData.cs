namespace WebApiProyectoDSII.Models
{
    public class ProyeccionData
    {
        public int MonthIndex { get; set; } // Representa el 'X' en la fórmula de mínimos cuadrados (índice secuencial del mes)
        public double Value { get; set; }    // Representa el 'Y' en la fórmula (cantidad de envíos o monto de ingresos)
        public DateTime Date { get; set; }  // La fecha del primer día del mes para referencia y visualización
    }
}
