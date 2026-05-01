using Microsoft.EntityFrameworkCore;
using VaccinationCenter.Data;
using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public class CompteService : ICompteService
    {
        private readonly ApplicationDbContext _context;

        public CompteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Compte?> AuthenticateAsync(string login, string password)
        {
            var compte = await _context.Comptes.FirstOrDefaultAsync(c => c.Login == login);
            if (compte == null) return null;
            return BCrypt.Net.BCrypt.Verify(password, compte.Password) ? compte : null;
        }

        public async Task<Compte> RegisterAsync(string login, string password)
        {
            var compte = new Compte
            {
                Login = login,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = Role.User
            };
            _context.Comptes.Add(compte);
            await _context.SaveChangesAsync();
            return compte;
        }

        public async Task<bool> LoginExistsAsync(string login)
            => await _context.Comptes.AnyAsync(c => c.Login == login);

        public async Task<IEnumerable<Compte>> GetAllAsync()
            => await _context.Comptes.ToListAsync();
    }
}
