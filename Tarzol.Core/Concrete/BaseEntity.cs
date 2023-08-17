using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Abstract;
using Tarzol.Core.Enums;

namespace Tarzol.Core.Concrete
{
   public class BaseEntity : IEntity, IBaseEntity<int>
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Status Status { get; set; }
        public int ID { get; set; }
    }
}
