using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersShop.Models.ProductsViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Typ")]
        public string Type { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Dost. ilosc")]
        public int Amount { get; set; }

        public virtual ICollection<ProductToOrder> ProductToOrders { get; set; }

    }
}
