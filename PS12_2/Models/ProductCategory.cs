using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS12_2.Models
{
    public class ProductCategory
    {
       
        public int ProductID { get; set; }
        public Product Product { get; set; }
        
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
