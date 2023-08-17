using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class ProductRatingManager: IProductRatingService
    {
        IProductRatingRepository _productRatingRepository;

        public ProductRatingManager(IProductRatingRepository productRatingRepository)
        {
            _productRatingRepository = productRatingRepository;
        }
        public bool Add(ProductRating item)
        {
            return _productRatingRepository.Insert(item);
        }

        public bool Delete(ProductRating item)
        {
            return _productRatingRepository.Remove(item);
        }

        public ProductRating GetBy(int id)
        {
            return _productRatingRepository.Get(id);
        }

        public List<ProductRating> GetListAll(Expression<Func<ProductRating, bool>> exception)
        {
            return _productRatingRepository.GetList(exception);
        }

        public List<ProductRating> GetListAll()
        {
            return _productRatingRepository.GetList();
        }

        public bool Update(ProductRating item)
        {
            return _productRatingRepository.Modified(item);
        }
    }
}
