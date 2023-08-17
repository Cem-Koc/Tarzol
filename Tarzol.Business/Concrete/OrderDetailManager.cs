using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class OrderDetailManager : IOrderDetailService
    {
        IOrderDetailRepository _orderDetailRepository;

        public OrderDetailManager(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public bool Add(OrderDetail item)
        {
            return _orderDetailRepository.Insert(item);
        }

        public bool Delete(OrderDetail item)
        {
            return _orderDetailRepository.Remove(item);
        }

        public OrderDetail GetBy(int id)
        {
            return _orderDetailRepository.Get(id);
        }

        public List<OrderDetail> GetListAll(Expression<Func<OrderDetail, bool>> exception)
        {
            return _orderDetailRepository.GetList(exception);
        }

        public List<OrderDetail> GetListAll()
        {
            return _orderDetailRepository.GetList();
        }

        public bool Update(OrderDetail item)
        {
            return _orderDetailRepository.Modified(item);
        }
    }
}
