using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlowersShop.Models.ProductsViewModels;

namespace FlowersShop.Models
{
    public class ProductToOrder
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        [Display(Name = "Ilość")]
        public int AmountOfProduct { get; set; }

        public double TotalPriceForThisProduct { get; set; }
        
        public ProductViewModel Product { get; set; }

        public Order Order { get; set; }


    }
}
