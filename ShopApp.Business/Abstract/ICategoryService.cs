using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int Id);
        List<Category> GetAll();
        void Create(Category entity);
        void Delete(Category entity);
        void Update(Category entity);
    }
}
