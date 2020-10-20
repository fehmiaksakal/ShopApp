using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(9900, MinimumLength = 3, ErrorMessage = "Ürün Adı Minimumu 3 karakter olmalı")]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }
        public string[] CategoryIds { get; set; }
        [Required(ErrorMessage ="Fiyat Alınamadı")]
        [Range(1,10000)]
        public decimal? Price { get; set; }
        public List<Category> SelectedCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
}
