using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    //This class for Junction process
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } //This name's "navigation prop".
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
