using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Lab10.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Completed { get; set; }
        public ICollection<OrderLineItem> OrderItems { get; set; } = new HashSet<OrderLineItem>();

        [NotMapped]
        public IEnumerable<Product> Products => OrderItems.Select(o => o.Product);
    }
}
