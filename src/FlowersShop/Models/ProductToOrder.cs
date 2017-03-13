using System;
using System.Collections.Generic;
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

        public int AmountOfProduct { get; set; }

        public double TotalPriceForThisProduct { get; set; }
        
        public virtual ProductViewModel Product { get; set; }

        public virtual Order Order { get; set; }


    }
}
