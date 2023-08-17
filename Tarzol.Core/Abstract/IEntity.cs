using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Enums;

namespace Tarzol.Core.Abstract
{
   public interface IEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        Status Status { get; set; }
    }
}
