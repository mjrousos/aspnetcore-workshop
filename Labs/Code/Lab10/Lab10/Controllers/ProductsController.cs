using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab10.Controllers
{
    public class ProductsController : Controller
    {
        private OrdersContext _dataContext;

        public ProductsController(OrdersContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> View(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Edit", product);
        }

        [HttpPost]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Save([FromForm]Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", product);
            }

            var update = _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();
            return Redirect("~/productlist");
        }

        [HttpGet]
        [Route("/api/[controller]/")]
        public async Task<IEnumerable<Product>> Get() =>
            await _dataContext.Products.ToListAsync();

        [HttpGet]
        [Route("/api/[controller]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("/api/[controller]")]
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
