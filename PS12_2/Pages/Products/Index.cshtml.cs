using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PS12_2.Data;
using PS12_2.Models;

namespace PS12_2.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly PS12_2.Data.ShopContext _context;

        public IndexModel(PS12_2.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
           
            Product =
                await _context.Products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).
                ToListAsync();
            
        }
    }
}
