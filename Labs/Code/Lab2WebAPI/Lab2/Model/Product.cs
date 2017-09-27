using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
