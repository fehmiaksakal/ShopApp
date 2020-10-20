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
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM PRODUCTCATEGORY WHERE ProductId = @p0 AND CategoryId = @p1";
                int resultSet = context.Database.ExecuteSqlRaw(cmd, productId, categoryId);

            }
        }

        public Category GetByIdWithProducts(int Id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                    .Where(x => x.Id == Id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
            }
        }
    }
}
