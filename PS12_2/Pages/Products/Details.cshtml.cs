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
    public class DetailsModel : PageModel
    {
        private readonly PS12_2.Data.ShopContext _context;

        public DetailsModel(PS12_2.Data.ShopContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.Include(x => x.ProductCategories).ThenInclude(x => x.Category).
                FirstOrDefaultAsync(m => m.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
