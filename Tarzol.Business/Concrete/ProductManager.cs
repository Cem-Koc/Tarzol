using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Add(Product item)
        {
            return _productRepository.Insert(item);
        }

        public bool Delete(Product item)
        {
            return _productRepository.Remove(item);
        }

        public Product GetBy(int id)
        {
            return _productRepository.Get(id);
        }

        public Product GetByIdProduct(int productid)
        {
            return _productRepository.GetById(productid);
        }

        public List<Product> GetListAll(Expression<Func<Product, bool>> exception)
        {
            return _productRepository.GetList(exception);
        }

        public List<Product> GetListAll()
        {
            return _productRepository.GetList();
        }

        

        public bool Update(Product item)
        {
            return _productRepository.Modified(item);
        }


        public List<Product> GetProductByID(int id)
        {
            return _productRepository.GetList(x => x.ID == id);
        }
    }
}
