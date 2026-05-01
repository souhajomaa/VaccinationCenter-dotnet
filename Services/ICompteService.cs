using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public interface ICompteService
    {
        Task<Compte?> AuthenticateAsync(string login, string password);
        Task<Compte> RegisterAsync(string login, string password);
        Task<bool> LoginExistsAsync(string login);
        Task<IEnumerable<Compte>> GetAllAsync();
    }
}
