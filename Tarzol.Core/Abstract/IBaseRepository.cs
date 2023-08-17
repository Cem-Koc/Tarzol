using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tarzol.Core.Abstract
{
   public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        bool Insert(T item);
        bool Modified(T item);
        bool Remove(T item);
        List<T> GetList(Expression<Func<T, bool>> exception);
        List<T> GetList();
        T Get(int id);
    }
}
