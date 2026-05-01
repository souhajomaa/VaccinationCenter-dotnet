using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public class Addresse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdresseId { get; set; }

        [Required(ErrorMessage = "La rue est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "Le numéro de rue doit être positif")]
        public int Rue { get; set; }

        [Required(ErrorMessage = "Le code postal est obligatoire")]
        [Range(1000, 99999, ErrorMessage = "Code postal invalide")]
        public int CodePostal { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La ville doit avoir entre 2 et 100 caractères")]
        public string Ville { get; set; } = string.Empty;

        // Navigation
        public ICollection<Citoyen> Citoyens { get; set; } = new List<Citoyen>();
    }
}
