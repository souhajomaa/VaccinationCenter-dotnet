using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationCenter.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public class Compte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le login est obligatoire")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login doit avoir entre 3 et 50 caractères")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le rôle est obligatoire")]
        public Role Role { get; set; } = Role.User;
    }
}
