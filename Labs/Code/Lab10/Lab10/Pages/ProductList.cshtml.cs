using Lab10.Data;
using Lab10.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab10.Pages
{
    public class ProductListModel : PageModel
    {
        private readonly OrdersContext _ordersContext;

        public ProductListModel(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public IEnumerable<Product> Products { get; private set; }

        public async Task OnGetAsync()
        {
            Products = await _ordersContext.Products.ToListAsync().ConfigureAwait(false);
        }
    }
}