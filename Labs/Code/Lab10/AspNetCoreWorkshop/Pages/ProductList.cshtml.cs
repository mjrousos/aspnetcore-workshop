using AspNetCoreWorkshop.Data;
using AspNetCoreWorkshop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreWorkshop.Pages
{
    public class ProductListModel : PageModel
    {
        private readonly StoreContext _storeContext;

        public ProductListModel(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IEnumerable<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _storeContext.Products.ToListAsync();
        }
    }
}