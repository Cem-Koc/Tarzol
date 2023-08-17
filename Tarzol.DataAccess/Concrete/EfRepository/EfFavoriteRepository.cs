using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.DataAccess.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.DataAccess.Repositories;
using Tarzol.Entity;

namespace Tarzol.DataAccess.Concrete.EfRepository
{
   public class EfFavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        private readonly TarzolDbContext _tarzolDbContext;

        public EfFavoriteRepository(TarzolDbContext tarzolDbContext) : base(tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }
    }
}
