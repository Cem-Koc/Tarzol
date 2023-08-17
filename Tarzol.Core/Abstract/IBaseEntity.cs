using System;
using System.Collections.Generic;
using System.Text;


namespace Tarzol.Core.Abstract
{
   public interface IBaseEntity<T>
    {
        public T ID { get; set; }
       
    }
}
