using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Completed { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; } = new HashSet<OrderLineItem>();

        [NotMapped]
        public IEnumerable<Product> Products => OrderLineItems.Select(o => o.Product).Distinct();
    }
}
