namespace WebApiProyectoDSII.Models
{
    public class ProyeccionResult
    {
        public List<ProyeccionData> HistoricalData { get; set; } = new List<ProyeccionData>();
        public List<ProyeccionData> ProjectedData { get; set; } = new List<ProyeccionData>();
        public string ProjectionType { get; set; } = string.Empty;
        public double SlopeA { get; set; }
        public double InterceptB { get; set; }
    }
}
