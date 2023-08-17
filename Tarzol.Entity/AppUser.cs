using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Abstract;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
    public class AppUser : IdentityUser<int>,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Status Status { get; set; }
        



        public virtual List<Customer> Customers { get; set; }
        public virtual List<ProductRating> ProductRatings { get; set; }
    }
}
