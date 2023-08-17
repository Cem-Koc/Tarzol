using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tarzol.Business.Abstract
{
   public interface IGenericService<T>
    {
        bool Add(T item);
        bool Update(T item);
        bool Delete(T item);
        List<T> GetListAll(Expression<Func<T, bool>> exception);
        List<T> GetListAll();
        T GetBy(int id);
    }
}
