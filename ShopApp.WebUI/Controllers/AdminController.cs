using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;
using System.Linq;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult ProductList()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price
            };

            _productService.Create(entity);

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult EditProduct(int? Id)
        {
            if (Id == null) return RedirectToAction("ProductList");

            var entity = _productService.GetByIdWithCategories(Id.Value);

            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                SelectedCategories = entity.ProductCategories.Select(x => x.Category).ToList(),
                Categories = _categoryService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            var entity = _productService.GetById(model.Id);

            if (entity == null) return NotFound();

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.ImageUrl = model.ImageUrl;
            entity.Price = model.Price;

            _productService.UpdateWithCategories(entity, model.CategoryIds);

            return RedirectToAction("ProductList");
        }


        [HttpPost]
        public IActionResult DeleteProduct(int Id)
        {
            var entity = _productService.GetById(Id);

            if (entity == null) return NotFound();

            _productService.Delete(entity);

            return RedirectToAction("ProductList");
        }


        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {

            if (model == null) return NotFound();

            _categoryService.Create(new Category()
            {
                Name = model.Name
            });

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult EditCategory(int? Id)
        {
            if (Id == null) return RedirectToAction("CategoryList");

            var entity = _categoryService.GetByIdWithProducts(Id.Value);

            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.ProductCategories.Select(x => x.Product).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);

            if (entity == null) return NotFound();

            entity.Name = model.Name;

            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int Id)
        {
            var entity = _categoryService.GetById(Id);

            if (entity == null) return NotFound();

            _categoryService.Delete(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {

            _categoryService.DeleteFromCategory(productId, categoryId);

            return Redirect("/Admin/editcategory/" + categoryId);
        }
    }
}
