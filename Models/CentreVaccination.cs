using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public class CentreVaccination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CentreVaccinationId { get; set; }

        [Required(ErrorMessage = "La capacité est obligatoire")]
        [Range(1, 10000, ErrorMessage = "La capacité doit être entre 1 et 10000")]
        public int Capacite { get; set; }

        [Required(ErrorMessage = "Le nombre de chaises est obligatoire")]
        [Range(1, 1000, ErrorMessage = "Nombre de chaises invalide")]
        public int NbChaises { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire")]
        [Range(10000000, 99999999, ErrorMessage = "Numéro de téléphone invalide")]
        public int NumTelephone { get; set; }

        [Required(ErrorMessage = "Le responsable est obligatoire")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nom du responsable invalide")]
        public string ResponsableCentre { get; set; } = string.Empty;

        // Navigation
        public ICollection<Vaccin> Vaccins { get; set; } = new List<Vaccin>();
    }
}
