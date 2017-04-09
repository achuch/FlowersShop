using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersShop.Models.ProductsViewModels;

namespace FlowersShop.Models
{
    public class ProductsInOrderViewModel
    {
        public ProductToOrder ProductToOrder { get; set; }

        public ProductViewModel ProductViewModel { get; set; }

        public string Message { get; set; }
    }
}
