using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null) return NotFound();

            Product product = _productService.GetProductDetails((int)Id);

            if (product == null) return NotFound();

            return View(new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(x => x.Category).ToList()
            });
        }
        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 2;
            return View(new ProductListModel()
            {
                PagingInfo = new PagingInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pageSize),
            });
        }



    }
}
