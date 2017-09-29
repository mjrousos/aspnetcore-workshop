using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab10.Controllers;
using Lab10.Data;
using Foo.Models;
using Microsoft.EntityFrameworkCore;

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