using VaccinationCenter.Models;

namespace VaccinationCenter.Services
{
    public interface IVaccinService : IGenericRepository<Vaccin>
    {
        Task<IEnumerable<Vaccin>> SearchByDateAsync(DateTime date);
        Task<IEnumerable<Vaccin>> SearchByTypeAsync(TypeVaccin type);
        Task<IEnumerable<Vaccin>> SearchByFournisseurAsync(string fournisseur);
        Task<IEnumerable<Vaccin>> GetAvailableVaccinsAsync();
        Task<IEnumerable<Vaccin>> GetWithCentreAsync();
    }
}
