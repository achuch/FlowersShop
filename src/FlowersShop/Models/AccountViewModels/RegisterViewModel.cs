using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersShop.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} i minimalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła się nie pokrywają")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display (Name = "Imie")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

    }
}
