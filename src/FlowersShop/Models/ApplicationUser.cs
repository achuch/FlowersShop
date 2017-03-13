using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlowersShop.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Czy ma uprawnienia admina?")]
        public bool Type { get; set; }

        [Display(Name = "Imie")]
        public string LastName { get; set; }

        [Display(Name = "Nazwisko")]
        public string FirstName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
