using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class OrderManager : IOrderService
    {
        IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public bool Add(Order item)
        {
            return _orderRepository.Insert(item);
        }

        public bool Delete(Order item)
        {
            return _orderRepository.Remove(item);
        }

        public Order GetBy(int id)
        {
            return _orderRepository.Get(id);
        }

        public List<Order> GetListAll(Expression<Func<Order, bool>> exception)
        {
            return _orderRepository.GetList(exception);
        }

        public List<Order> GetListAll()
        {
            return _orderRepository.GetList();
        }

        public bool Update(Order item)
        {
            return _orderRepository.Modified(item);
        }
    }
}
