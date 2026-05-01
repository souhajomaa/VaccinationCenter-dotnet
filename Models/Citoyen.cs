using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public class Citoyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CiToyenId { get; set; }

        [Required(ErrorMessage = "Le CIN est obligatoire")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "CIN invalide (8-20 caractères)")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "CIN ne doit contenir que des lettres et chiffres")]
        public string CIN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Nom invalide")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prénom invalide")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'âge est obligatoire")]
        [Range(1, 150, ErrorMessage = "L'âge doit être entre 1 et 150")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        [Range(10000000, 99999999, ErrorMessage = "Numéro de téléphone invalide (8 chiffres)")]
        public int Telephone { get; set; }

        [Required(ErrorMessage = "Le numéro EVAX est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "Numéro EVAX invalide")]
        public int NumeroEvax { get; set; }

        // FK
        [ForeignKey("Addresse")]
        public int AdresseId { get; set; }
        public Addresse? Addresse { get; set; }

        // Navigation
        public ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
    }
}
