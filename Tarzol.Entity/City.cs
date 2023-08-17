using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class City : BaseEntity
    {
        public string CityName { get; set; }

        public virtual List<District> Districts { get; set; }


    }
}
