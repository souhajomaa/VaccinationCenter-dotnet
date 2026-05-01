namespace VaccinationCenter.Services
{
    public class StatistiquesDto
    {
        public int TotalCitoyens { get; set; }
        public int TotalVaccins { get; set; }
        public int TotalRendezVous { get; set; }
        public int TotalCentres { get; set; }
        public Dictionary<string, int> VaccinsByType { get; set; } = new();
        public Dictionary<string, int> RendezVousByMonth { get; set; } = new();
        public int VaccinsDisponibles { get; set; }
        public int VaccinsExpires { get; set; }
    }

    public interface IStatistiquesService
    {
        Task<StatistiquesDto> GetStatistiquesAsync();
    }
}
