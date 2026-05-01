using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Data;
using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public class RendezVousService : GenericRepository<RendezVous>, IRendezVousService
    {
        public RendezVousService(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<RendezVous>> GetAllWithDetailsAsync()
            => await _dbSet
                .Include(r => r.Citoyen)
                .Include(r => r.Vaccin)
                .ThenInclude(v => v!.CentreVaccination)
                .ToListAsync();

        public async Task<IEnumerable<RendezVous>> GetByCitoyenAsync(int citoyenId)
            => await _dbSet
                .Include(r => r.Citoyen)
                .Include(r => r.Vaccin)
                .Where(r => r.CiToyenId == citoyenId)
                .ToListAsync();

        public async Task<IEnumerable<RendezVous>> GetByDateAsync(DateTime date)
            => await _dbSet
                .Include(r => r.Citoyen)
                .Include(r => r.Vaccin)
                .Where(r => r.DateVaccination.Date == date.Date)
                .ToListAsync();
    }
}
