using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWorkshop.Models;
using AspNetCoreWorkshop.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWorkshop.Controllers
{
    public class ProductsController : Controller
    {
        private StoreContext _dataContext;

        public ProductsController(StoreContext storeContext)
        {
            _dataContext = storeContext;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(nameof(Edit), product);
        }

        [HttpPost]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Save([FromForm]Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), product);
            }

            var update = _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();

            return Redirect("~/productlist");
        }

        [HttpGet]
        [Route("/api/[controller]")]
        public async Task<IEnumerable<Product>> Get() => await _dataContext.Products.ToListAsync();

        [HttpGet]
        [Route("/api/[controller]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _dataContext.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("/api/[controller]")]
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
