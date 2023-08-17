using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentRepository _commentRepository;

        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool Add(Comment item)
        {
            return _commentRepository.Insert(item);
        }

        public bool Delete(Comment item)
        {
            throw new NotImplementedException();
        }

        public Comment GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetListAll(Expression<Func<Comment, bool>> exception)
        {
            return _commentRepository.GetList(exception);
        }
        public List<Comment> GetListAll(int id)
        {
            return _commentRepository.GetList(x=>x.ProductID==id);
        }
        public List<Comment> GetListAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment item)
        {
            throw new NotImplementedException();
        }
    }
}
