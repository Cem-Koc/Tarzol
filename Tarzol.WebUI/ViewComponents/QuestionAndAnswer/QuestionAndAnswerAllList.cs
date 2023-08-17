using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.ViewComponents.QuestionAndAnswer
{
    public class QuestionAndAnswerAllList : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;
        IQuestionService _questionService;
        public QuestionAndAnswerAllList(TarzolDbContext tarzolDbContext, IQuestionService questionService)
        {
            _tarzolDbContext = tarzolDbContext;
            _questionService = questionService;
           
        }

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.productId = id;
            var questions = _questionService.GetListAll(x => x.ProductID == id);
            var answer = _tarzolDbContext.Answers.Where(İ => İ.ProductID == id).ToList();
            var product = _tarzolDbContext.Products.Where(i => i.ID == id).FirstOrDefault();
            var seller = _tarzolDbContext.Sellers.Where(i => i.ID == product.SellerID).FirstOrDefault();
            QuestionAndAnswerList questionAndAnswerList = new QuestionAndAnswerList()
            {
                Questions = questions,
                Answers = answer,
                Seller=seller
            };
            return View(questionAndAnswerList);
        }
    }
}
