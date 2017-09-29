using Foo.Models;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWorkshop.Models
{
    public class OrderLineItem
    {
        public int Id { get; set; }
        [Range(1, 100)] public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
