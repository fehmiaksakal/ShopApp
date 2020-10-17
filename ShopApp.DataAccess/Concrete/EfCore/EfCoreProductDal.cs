using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public Product GetByIdWithCategories(int Id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Where(x => x.Id == Id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Count();
            }
        }

        public Product GetProductDetails(int Id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Where(x => x.Id == Id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void UpdateWithCategories(Product entity, string[] catArray)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                .Include(x => x.ProductCategories)
                                .FirstOrDefault(x => x.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    if (catArray != null)
                    {
                        product.ProductCategories = catArray
                                               .Select(
                                                       x => new ProductCategory()
                                                       {
                                                           CategoryId = Convert.ToInt32(x),
                                                           ProductId = entity.Id
                                                       }).ToList();
                    }

                    context.SaveChanges();
                }

            }
        }
    }
}
