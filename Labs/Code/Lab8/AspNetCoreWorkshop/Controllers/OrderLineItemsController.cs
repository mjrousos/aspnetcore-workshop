using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWorkshop.Data;
using AspNetCoreWorkshop.Models;

namespace AspNetCoreWorkshop.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderLineItems")]
    public class OrderLineItemsController : Controller
    {
        private readonly StoreContext _context;

        public OrderLineItemsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderLineItems
        [HttpGet]
        public IEnumerable<OrderLineItem> GetOrderLineItems()
        {
            return _context.OrderLineItems;
        }

        // GET: api/OrderLineItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderLineItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderLineItem = await _context.OrderLineItems.SingleOrDefaultAsync(m => m.Id == id);

            if (orderLineItem == null)
            {
                return NotFound();
            }

            return Ok(orderLineItem);
        }

        // PUT: api/OrderLineItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderLineItem([FromRoute] int id, [FromBody] OrderLineItem orderLineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderLineItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderLineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderLineItems
        [HttpPost]
        public async Task<IActionResult> PostOrderLineItem([FromBody] OrderLineItem orderLineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderLineItems.Add(orderLineItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLineItem", new { id = orderLineItem.Id }, orderLineItem);
        }

        // DELETE: api/OrderLineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLineItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderLineItem = await _context.OrderLineItems.SingleOrDefaultAsync(m => m.Id == id);
            if (orderLineItem == null)
            {
                return NotFound();
            }

            _context.OrderLineItems.Remove(orderLineItem);
            await _context.SaveChangesAsync();

            return Ok(orderLineItem);
        }

        private bool OrderLineItemExists(int id)
        {
            return _context.OrderLineItems.Any(e => e.Id == id);
        }
    }
}