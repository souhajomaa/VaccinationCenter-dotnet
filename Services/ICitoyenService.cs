using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public interface ICitoyenService : IGenericRepository<Citoyen>
    {
        Task<Citoyen?> GetByCINAsync(string cin);
        Task<IEnumerable<Citoyen>> GetWithAddresseAsync();
        Task<IEnumerable<RendezVous>> GetRendezVousByCitoyenAsync(int citoyenId);
    }
}
