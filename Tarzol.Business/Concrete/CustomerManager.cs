using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class CustomerManager : ICustomerService
    {
        ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool Add(Customer item)
        {
            return _customerRepository.Insert(item);
        }

        public bool Delete(Customer item)
        {
            return _customerRepository.Remove(item);
        }

        public Customer GetBy(int id)
        {
            return _customerRepository.Get(id);
        }

        public List<Customer> GetListAll(Expression<Func<Customer, bool>> exception)
        {
            return _customerRepository.GetList(exception);
        }

        public List<Customer> GetListAll()
        {
            return _customerRepository.GetList();
        }

        public bool Update(Customer item)
        {
            return _customerRepository.Modified(item);
        }
    }
}
