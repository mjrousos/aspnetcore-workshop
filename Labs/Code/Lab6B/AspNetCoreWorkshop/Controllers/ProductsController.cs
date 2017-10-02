using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Models;
using AspNetCoreWorkshop.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AspNetCoreWorkshop.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private StoreContext _dataContext;

        public ProductsController(StoreContext storeContext)
        {
            _dataContext = storeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get() => await _dataContext.Products.ToListAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Log.Information("GET {Id}: Found {@Product}", id, product);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
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
