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

        public bool IsFinished { get; set; }

        public bool IsRealized { get; set; }

        public DateTime? DateOfRealize { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ProductToOrder> ProductToOrders { get; set; }
    }
}
