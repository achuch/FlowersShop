using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int TotalPrice { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ProductToOrder> ProductToOrders { get; set; }
    }
}
