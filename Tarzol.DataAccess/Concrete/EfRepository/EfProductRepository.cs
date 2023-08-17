using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarzol.DataAccess.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.DataAccess.Repositories;
using Tarzol.Entity;

namespace Tarzol.DataAccess.Concrete.EfRepository
{
   public class EfProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly TarzolDbContext _tarzolDbContext;

        public EfProductRepository(TarzolDbContext tarzolDbContext) : base(tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public Product GetById(int productid)
        {
            return _tarzolDbContext.Products
                .Include(i => i.Category)
                .ThenInclude(i => i.CategoryAndSubCategories)
                .ThenInclude(i => i.SubCategory)
                .FirstOrDefault(i => i.ID == productid);
        }
    }
}
