using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Data;
using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public class CitoyenService : GenericRepository<Citoyen>, ICitoyenService
    {
        public CitoyenService(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Citoyen>> GetAllAsync()
            => await _dbSet.Include(c => c.Addresse).ToListAsync();

        public async Task<Citoyen?> GetByCINAsync(string cin)
            => await _dbSet.Include(c => c.Addresse)
                .FirstOrDefaultAsync(c => c.CIN == cin);

        public async Task<IEnumerable<Citoyen>> GetWithAddresseAsync()
            => await _dbSet.Include(c => c.Addresse).ToListAsync();

        public async Task<IEnumerable<RendezVous>> GetRendezVousByCitoyenAsync(int citoyenId)
            => await _context.RendezVous
                .Include(r => r.Vaccin)
                .Where(r => r.CiToyenId == citoyenId)
                .ToListAsync();
    }
}
