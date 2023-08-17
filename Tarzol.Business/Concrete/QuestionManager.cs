using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Abstract;
using Tarzol.Entity;

namespace Tarzol.Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        IQuestionRepository _questionRepository;

        public QuestionManager(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public bool Add(Question item)
        {
            return _questionRepository.Insert(item);
        }

        public bool Delete(Question item)
        {
            throw new NotImplementedException();
        }

        public Question GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetListAll(Expression<Func<Question, bool>> exception)
        {
            return _questionRepository.GetList(exception);
        }

        public List<Question> GetListAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Question item)
        {
            throw new NotImplementedException();
        }
    }
}
