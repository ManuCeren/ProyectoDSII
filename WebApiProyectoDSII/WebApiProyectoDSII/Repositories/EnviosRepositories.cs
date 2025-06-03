using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProyectoDSII.Models;

namespace WebApiProyectoDSII.Repositories
{
    public class EnviosRepositories
    {
        private readonly TransporteFloresDbContext _context;

        public EnviosRepositories(TransporteFloresDbContext context)
        {
            _context = context;
        }


        public async Task<List<ProyeccionData>> GetMonthlyEnviosData(int monthsBack)
        {
            DateTime startDate = DateTime.Today.AddMonths(-monthsBack).Date;

            // Ejecutar consulta sin índice en SQL
            var queryResult = await _context.Envios
                .Where(e => e.FechaSolicitud.HasValue && e.FechaSolicitud.Value >= startDate)
                .GroupBy(e => new
                {
                    Year = e.FechaSolicitud!.Value.Year,
                    Month = e.FechaSolicitud!.Value.Month
                })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new ProyeccionData
                {
                    MonthIndex = 0, // temporal, se reemplazará después
                    Value = (double)g.Count(),
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1)
                })
                .ToListAsync();

            // Ahora asigna el índice en memoria
            var resultWithIndex = queryResult
                .Select((item, index) =>
                {
                    item.MonthIndex = index + 1;
                    return item;
                })
                .ToList();

            return resultWithIndex;
        }


        public async Task<List<ProyeccionData>> GetMonthlyIngresosData(int monthsBack)
        {
            DateTime startDate = DateTime.Today.AddMonths(-monthsBack).Date;

            var queryResult = await _context.Facturacions
                .Where(f => f.FechaFactura.HasValue && f.FechaFactura.Value >= startDate)
                .GroupBy(f => new
                {
                    Year = f.FechaFactura!.Value.Year,
                    Month = f.FechaFactura!.Value.Month
                })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new ProyeccionData
                {
                    MonthIndex = 0,
                    Value = (double)g.Sum(f => f.MontoTotal ?? 0),
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1)
                })
                .ToListAsync();

            var resultWithIndex = queryResult
                .Select((item, index) =>
                {
                    item.MonthIndex = index + 1;
                    return item;
                })
                .ToList();

            return resultWithIndex;
        }

    }
}


