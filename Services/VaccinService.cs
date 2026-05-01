using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Data;
using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public class VaccinService : GenericRepository<Vaccin>, IVaccinService
    {
        public VaccinService(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Vaccin>> GetAllAsync()
            => await _dbSet.Include(v => v.CentreVaccination).ToListAsync();

        public async Task<IEnumerable<Vaccin>> SearchByDateAsync(DateTime date)
            => await _dbSet.Include(v => v.CentreVaccination)
                .Where(v => v.DateValidite.Date >= date.Date)
                .ToListAsync();

        public async Task<IEnumerable<Vaccin>> SearchByTypeAsync(TypeVaccin type)
            => await _dbSet.Include(v => v.CentreVaccination)
                .Where(v => v.TypeVaccin == type)
                .ToListAsync();

        public async Task<IEnumerable<Vaccin>> SearchByFournisseurAsync(string fournisseur)
            => await _dbSet.Include(v => v.CentreVaccination)
                .Where(v => v.Fournisseur.Contains(fournisseur))
                .ToListAsync();

        public async Task<IEnumerable<Vaccin>> GetAvailableVaccinsAsync()
            => await _dbSet.Include(v => v.CentreVaccination)
                .Where(v => v.Quantite > 0 && v.DateValidite >= DateTime.Now)
                .ToListAsync();

        public async Task<IEnumerable<Vaccin>> GetWithCentreAsync()
            => await _dbSet.Include(v => v.CentreVaccination).ToListAsync();
    }
}
