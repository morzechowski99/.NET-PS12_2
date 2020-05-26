using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PS12_2.Data;
using PS12_2.Models;

namespace PS12_2.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly PS12_2.Data.ShopContext _context;

        public CreateModel(PS12_2.Data.ShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> ids { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.Products.Add(Product);
            
            await _context.SaveChangesAsync();
            foreach(var id in ids)
            {
                var ProductandCategory = new ProductCategory()
                {
                    ProductID = Product.Id,
                    CategoryID = id
                };
                _context.ProductCategories.Add(ProductandCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
