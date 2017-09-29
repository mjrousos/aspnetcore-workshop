using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Model;

namespace Lab2.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Computer" },
            new Product { Id = 2, Name = "Apple" },
            new Product { Id = 3, Name = "Orange" },
        };

        [HttpGet]
        public IEnumerable<Product> Get() => _products;

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
    }
}
