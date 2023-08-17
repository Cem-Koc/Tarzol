using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.Business.Abstract
{
   public interface IProductService : IGenericService<Product>
    {
        Product GetByIdProduct(int productid);
        List<Product> GetProductByID(int id);
    }
}
