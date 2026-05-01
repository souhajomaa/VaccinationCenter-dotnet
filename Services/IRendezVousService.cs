using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public interface IRendezVousService : IGenericRepository<RendezVous>
    {
        Task<IEnumerable<RendezVous>> GetAllWithDetailsAsync();
        Task<IEnumerable<RendezVous>> GetByCitoyenAsync(int citoyenId);
        Task<IEnumerable<RendezVous>> GetByDateAsync(DateTime date);
    }
}
