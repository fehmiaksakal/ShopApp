using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Create(Product entity)
        {
            _productDal.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll(null);
        }

        public Product GetById(int Id)
        {
            return _productDal.GetByıd(Id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public List<Product> GetPopularProducts()
        {
            return _productDal.GetAll(x=>x.Name.Contains("sa")).ToList();
        }

        public Product GetProductDetails(int Id)
        {
            return _productDal.GetProductDetails(Id);
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            return _productDal.GetProductsByCategory(category, page, pageSize).ToList();
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }
    }
}
