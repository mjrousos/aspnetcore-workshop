using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Lab10.Models;
using System.Threading.Tasks;

namespace Foo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        public ICollection<OrderLineItem> OrderItems { get; set; } = new HashSet<OrderLineItem>();

        [NotMapped]
        public IEnumerable<Order> Orders => OrderItems.Select(o => o.Order);
    }
}
