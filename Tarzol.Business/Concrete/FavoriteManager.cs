using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        IFavoriteRepository _favoriteRepository;

        public FavoriteManager(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public bool Add(Favorite item)
        {
            return _favoriteRepository.Insert(item);
        }

        public bool Delete(Favorite item)
        {
            return _favoriteRepository.Remove(item);
        }

        public Favorite GetBy(int id)
        {
            return _favoriteRepository.Get(id);
        }

        public List<Favorite> GetListAll(Expression<Func<Favorite, bool>> exception)
        {
            return _favoriteRepository.GetList(exception);
        }

        public List<Favorite> GetListAll()
        {
            return _favoriteRepository.GetList();
        }

        public bool Update(Favorite item)
        {
            return _favoriteRepository.Modified(item);
        }
    }
}
