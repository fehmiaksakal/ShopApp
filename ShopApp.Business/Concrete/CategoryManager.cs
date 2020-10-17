using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Create(Category entity)
        {
            _categoryDal.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryDal.Delete(entity);
        }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            _categoryDal.DeleteFromCategory(productId, categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll(null);
        }

        public Category GetById(int Id)
        {
            return _categoryDal.GetByıd(Id);
        }

        public Category GetByIdWithProducts(int Id)
        {
            return _categoryDal.GetByIdWithProducts(Id);
        }

        public void Update(Category entity)
        {
            _categoryDal.Update(entity);
        }
    }
}
