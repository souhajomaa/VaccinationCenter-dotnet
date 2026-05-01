using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public class Vaccin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaccinId { get; set; }

        [Required(ErrorMessage = "La date de validité est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de validité")]
        public DateTime DateValidite { get; set; }

        [Required(ErrorMessage = "Le fournisseur est obligatoire")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nom du fournisseur invalide")]
        public string Fournisseur { get; set; } = string.Empty;

        [Required(ErrorMessage = "La quantité est obligatoire")]
        [Range(1, 100000, ErrorMessage = "La quantité doit être positive")]
        public int Quantite { get; set; }

        [Required(ErrorMessage = "Le type de vaccin est obligatoire")]
        [Display(Name = "Type de vaccin")]
        public TypeVaccin TypeVaccin { get; set; }

        // FK
        [ForeignKey("CentreVaccination")]
        public int CentreVaccinationId { get; set; }
        public CentreVaccination? CentreVaccination { get; set; }

        // Navigation
        public ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
    }
}
