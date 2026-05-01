using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public class RendezVous
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RendezVousId { get; set; }

        [Required(ErrorMessage = "Le code infirmière est obligatoire")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Code infirmière invalide")]
        public string CodeInfirmiere { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de vaccination est obligatoire")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date de vaccination")]
        public DateTime DateVaccination { get; set; }

        [Required(ErrorMessage = "Le nombre de doses est obligatoire")]
        [Range(1, 10, ErrorMessage = "Le nombre de doses doit être entre 1 et 10")]
        [Display(Name = "Nombre de doses")]
        public int NbrDoses { get; set; }

        // FK
        [ForeignKey("Citoyen")]
        public int CiToyenId { get; set; }
        public Citoyen? Citoyen { get; set; }

        [ForeignKey("Vaccin")]
        public int VaccinId { get; set; }
        public Vaccin? Vaccin { get; set; }
    }
}
