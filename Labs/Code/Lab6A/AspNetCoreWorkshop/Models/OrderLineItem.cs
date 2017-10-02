using AspNetCoreWorkshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Models
{
    public class OrderLineItem
    {
        public int Id { get; set; }
        [Range(1, 100)] public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
