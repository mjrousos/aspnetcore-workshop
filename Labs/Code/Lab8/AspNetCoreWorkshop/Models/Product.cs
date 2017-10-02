using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        public ICollection<OrderLineItem> OrderLineItems = new HashSet<OrderLineItem>();

        public IEnumerable<Order> Orders => OrderLineItems.Select(o => o.Order).Distinct();
    }
}
