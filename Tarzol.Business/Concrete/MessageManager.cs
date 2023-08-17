using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class MessageManager : IMessageService
    {
        IMessageRepository _messageRepository;

        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public bool Add(Message item)
        {
            return _messageRepository.Insert(item);
        }

        public bool Delete(Message item)
        {
            return _messageRepository.Remove(item);
        }

        public Message GetBy(int id)
        {
            return _messageRepository.Get(id);
        }

        public List<Message> GetListAll(Expression<Func<Message, bool>> exception)
        {
            return _messageRepository.GetList(exception);
        }

        public List<Message> GetListAll()
        {
            return _messageRepository.GetList();
        }

        public bool Update(Message item)
        {
            return _messageRepository.Modified(item);
        }
    }
}
