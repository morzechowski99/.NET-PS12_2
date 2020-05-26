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
    public class EditModel : PageModel
    {
        private readonly PS12_2.Data.ShopContext _context;

        public EditModel(PS12_2.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<int> ids { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Categories = await _context.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }

            Product = Product = await _context.Products.Include(x => x.ProductCategories).
                FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            foreach(var productcategory in _context.ProductCategories.ToList())
            {
                if(productcategory.ProductID == Product.Id)
                _context.Remove(productcategory);
            }
            
            foreach (var id in ids)
            {
                var ProductandCategory = new ProductCategory()
                {
                    ProductID = Product.Id,
                    CategoryID = id
                };
                _context.ProductCategories.Add(ProductandCategory);
            }
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
