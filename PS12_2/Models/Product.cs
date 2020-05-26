using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS12_2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        [Required]
        public decimal Price { get; set; }
        public  ICollection<ProductCategory> ProductCategories { get; set; }
        
    }
}
