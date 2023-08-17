using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class CategoryManager:ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool Add(Category item)
        {
            return _categoryRepository.Insert(item);
        }

        public bool Delete(Category item)
        {
            return _categoryRepository.Remove(item);
        }

        public Category GetBy(int id)
        {
            return _categoryRepository.Get(id);
        }

        public List<Category> GetListAll(Expression<Func<Category, bool>> exception)
        {
            return _categoryRepository.GetList(exception);
        }

        public List<Category> GetListAll()
        {
            return _categoryRepository.GetList();
        }

        public bool Update(Category item)
        {
            return _categoryRepository.Modified(item);
        }
    }
}
