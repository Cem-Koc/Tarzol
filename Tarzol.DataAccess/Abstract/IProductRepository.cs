using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tarzol.Core.Abstract;
using Tarzol.Entity;

namespace Tarzol.DataAccess.Abstract
{
   public interface IProductRepository : IBaseRepository<Product>
    {
        Product GetById(int productid);
    }
}
