using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
   public class NotificationManager : INotificationService
    {
        INotificationRepository _notificationRepository;

        public NotificationManager(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public bool Add(Notification item)
        {
            return _notificationRepository.Insert(item);
        }

        public bool Delete(Notification item)
        {
            return _notificationRepository.Remove(item);
        }

        public Notification GetBy(int id)
        {
            return _notificationRepository.Get(id);
        }

        public List<Notification> GetListAll(Expression<Func<Notification, bool>> exception)
        {
            return _notificationRepository.GetList(exception);
        }

        public List<Notification> GetListAll()
        {
            return _notificationRepository.GetList();
        }

        public bool Update(Notification item)
        {
            return _notificationRepository.Modified(item);
        }
    }
}
