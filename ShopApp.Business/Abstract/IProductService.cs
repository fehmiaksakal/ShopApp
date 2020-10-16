﻿using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int Id);
        Product GetProductDetails(int Id);
        List<Product> GetAll();
        List<Product> GetPopularProducts();
        List<Product> GetProductsByCategory(string category, int page, int pageSize);
        void Create(Product entity);
        void Delete(Product entity);
        void Update(Product entity);
        int GetCountByCategory(string category);
    }
}
