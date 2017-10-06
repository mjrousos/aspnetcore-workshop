using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Models;
using AspNetCoreWorkshop.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWorkshop.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly StoreContext _dataContext;

        public ProductsController(StoreContext storeContext, ILogger<ProductsController> logger)
        {
            _logger = logger;
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

            _logger.LogInformation("GET {Id}: Found {@Product}", id, product);

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
