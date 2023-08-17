using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Core.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        private TarzolDbContext _tarzolDbContext;

        public BaseRepository(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public bool Insert(T item)
        {
            try
            {
                _tarzolDbContext.Set<T>().Add(item);
                var ess = _tarzolDbContext.SaveChanges();
                return ess > 0;
            }
            catch 
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            try
            {
                _tarzolDbContext.Remove(item);
                var ess = _tarzolDbContext.SaveChanges();
                return ess > 0;
            }
            catch 
            {

                return false;
            }
        }

        public T Get(int id)
        {
            return _tarzolDbContext.Set<T>().Find(id);
        }

        public List<T> GetList(Expression<Func<T, bool>> exception)
        {
            return _tarzolDbContext.Set<T>().Where(exception).ToList();
        }

        public List<T> GetList()
        {
            return _tarzolDbContext.Set<T>().ToList();
        }

        public bool Modified(T item)
        {
            try
            {
                _tarzolDbContext.Set<T>().Update(item);
                var ess = _tarzolDbContext.SaveChanges();
                return ess > 0;
            }
            catch 
            {

                return false;
            }
        }
    }
}
