using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Entity;

namespace Tarzol.Business.Abstract
{
   public interface ICommentService : IGenericService<Comment>
    {
        public List<Comment> GetListAll(int id);
    }
}
