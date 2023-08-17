using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
    public class SellerManager : ISellerService
    {
        ISellerRepository _sellerRepository;

        public SellerManager(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public bool Add(Seller item)
        {
            return _sellerRepository.Insert(item);
        }

        public bool Delete(Seller item)
        {
            return _sellerRepository.Remove(item);
        }

        public Seller GetBy(int id)
        {
            return _sellerRepository.Get(id);
        }

        public List<Seller> GetListAll(Expression<Func<Seller, bool>> exception)
        {
            return _sellerRepository.GetList(exception);
        }

        public List<Seller> GetListAll()
        {
            return _sellerRepository.GetList();
        }

        public bool Update(Seller item)
        {
            return _sellerRepository.Modified(item);
        }
    }
}
