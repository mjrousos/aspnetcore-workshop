using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab10.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class ProductsController: ControllerBase
    {
        private OrdersContext _dataContext;

        public ProductsController(OrdersContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get() =>
            await _dataContext.Products.ToListAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
    }
}
