using System;
using System.Collections.Generic;
using System.Linq;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Services
{
    public class ProyecctionService
    {
        /// <summary>
        /// Calcula la proyección de una serie de tiempo usando el método de mínimos cuadrados.
        /// </summary>
        /// <param name="historicalData">Lista de datos históricos mensuales (MonthIndex, Value).</param>
        /// <param name="monthsToProject">Número de meses futuros a proyectar.</param>
        /// <param name="projectionType">Tipo de proyección (ej. "Envios", "Ingresos").</param>
        /// <returns>Un objeto ProjectionResult con los datos históricos y proyectados.</returns>
        /// <exception cref="ArgumentException">Se lanza si los datos históricos son nulos o vacíos.</exception>
        public ProyeccionResult CalculateLeastSquaresProjection(List<ProyeccionData> historicalData, int monthsToProject, string projectionType)
        {
            if (historicalData == null || !historicalData.Any())
            {
                throw new ArgumentException("Los datos históricos no pueden ser nulos o vacíos para la proyección.");
            }

            // Calcular las sumas necesarias para las fórmulas de mínimos cuadrados
            // X = MonthIndex, Y = Value
            double sumX = historicalData.Sum(d => (double)d.MonthIndex);
            double sumY = historicalData.Sum(d => d.Value);
            double sumXY = historicalData.Sum(d => (double)d.MonthIndex * d.Value);
            double sumX2 = historicalData.Sum(d => (double)d.MonthIndex * d.MonthIndex);
            int n = historicalData.Count;

            // Calcular la pendiente (a) de la línea de regresión
            double slopeA = (double)(n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);

            // Calcular la intersección (b) con el eje Y
            double interceptB = (sumY - slopeA * sumX) / n;

            // Generar los datos proyectados para los meses futuros
            List<ProyeccionData> projectedData = new List<ProyeccionData>();

            // Obtener el último índice de mes y la última fecha de los datos históricos
            int lastMonthIndex = historicalData.Max(d => d.MonthIndex);
            DateTime lastDate = historicalData.Max(d => d.Date);

            for (int i = 1; i <= monthsToProject; i++)
            {
                int futureMonthIndex = lastMonthIndex + i; // El índice del mes futuro
                double rawProjectedValue = slopeA * futureMonthIndex + interceptB; // Calcular el valor proyectado

                // Asegurarse de que los valores proyectados no sean negativos (ej. no puedes tener -5 envíos)
                if (rawProjectedValue < 0)
                {
                    rawProjectedValue = 0;
                }

                //  Redondea el valor proyectado al entero más cercano 
                double roundedProjectedValue = Math.Round(rawProjectedValue, MidpointRounding.AwayFromZero);

                // Calcular la fecha correspondiente al mes futuro
                DateTime futureDate = lastDate.AddMonths(i);

                projectedData.Add(new ProyeccionData
                {
                    MonthIndex = futureMonthIndex,
                    Value = roundedProjectedValue, // Usa el valor redondeado
                    Date = futureDate
                });
            }

            return new ProyeccionResult
            {
                HistoricalData = historicalData,
                ProjectedData = projectedData,
                ProjectionType = projectionType,
                SlopeA = slopeA,
                InterceptB = interceptB
            };
        }
    }
}