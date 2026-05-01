using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Data;

namespace VaccinationCenter.Services
{
    public class StatistiquesService : IStatistiquesService
    {
        private readonly ApplicationDbContext _context;
        public StatistiquesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StatistiquesDto> GetStatistiquesAsync()
        {
            var now = DateTime.Now;

            var vaccinsByType = _context.Vaccins
                .AsEnumerable()
                .GroupBy(v => v.TypeVaccin.ToString())
                .Select(g => new { Type = g.Key, Count = g.Sum(v => v.Quantite) })
                .ToList();

            var rdvByMonth = _context.RendezVous
                .AsEnumerable()
                .Where(r => r.DateVaccination.Year == now.Year)
                .GroupBy(r => r.DateVaccination.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToList();

            var monthNames = new[] { "", "Jan", "Fév", "Mar", "Avr", "Mai", "Jun",
                                      "Jul", "Aoû", "Sep", "Oct", "Nov", "Déc" };

            return new StatistiquesDto
            {
                TotalCitoyens = await _context.Citoyens.CountAsync(),
                TotalVaccins = await _context.Vaccins.SumAsync(v => v.Quantite),
                TotalRendezVous = await _context.RendezVous.CountAsync(),
                TotalCentres = await _context.CentresVaccination.CountAsync(),
                VaccinsByType = vaccinsByType.ToDictionary(x => x.Type, x => x.Count),
                RendezVousByMonth = rdvByMonth.ToDictionary(x => monthNames[x.Month], x => x.Count),
                VaccinsDisponibles = await _context.Vaccins.CountAsync(v => v.Quantite > 0 && v.DateValidite >= now),
                VaccinsExpires = await _context.Vaccins.CountAsync(v => v.DateValidite < now)
            };
        }
    }
}