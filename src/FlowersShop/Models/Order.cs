using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Całkowita cena")]
        public double TotalPrice { get; set; }

        [Display(Name = "Czy zakończono")]
        public bool IsFinished { get; set; }

        [Display(Name = "Czy zrealizowano")]
        public bool IsRealized { get; set; }

        public DateTime? DateOfRealize { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime? DateOfFinished { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Produkty")]
        public List<ProductToOrder> ProductToOrders { get; set; }

        [Display(Name = "Miasto")]
        public string AddressCity { get; set; }

        [Display(Name = "Ulica")]
        public string AddressStreet { get; set; }

        [Display(Name = "Numer domu")]
        public int AdressHouseNumber { get; set; }

        [Display(Name = "Numer mieszkania")]
        public int? AddressLocalNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string AddressZipCode { get; set; }
    }
}
