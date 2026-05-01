using System.ComponentModel.DataAnnotations;
using VaccinationCenter.Models;

namespace VaccinationCenter.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Le login est obligatoire")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le login est obligatoire")]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Minimum 6 caractères")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation est obligatoire")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        [Display(Name = "Confirmer le mot de passe")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class VaccinSearchViewModel
    {
        public DateTime? Date { get; set; }
        public TypeVaccin? Type { get; set; }
        public string? Fournisseur { get; set; }
        public IEnumerable<Vaccin> Results { get; set; } = new List<Vaccin>();
    }

    public class ReservationViewModel
    {
        [Required]
        public int VaccinId { get; set; }

        [Required(ErrorMessage = "Le CIN est obligatoire")]
        public string CIN { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date est obligatoire")]
        [DataType(DataType.DateTime)]
        public DateTime DateVaccination { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Le code infirmière est obligatoire")]
        public string CodeInfirmiere { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int NbrDoses { get; set; } = 1;

        public Vaccin? Vaccin { get; set; }
    }
}
