using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IQuestionService _questionService;

        public QuestionController(TarzolDbContext tarzolDbContext, IQuestionService questionService)
        {
            _tarzolDbContext = tarzolDbContext;
            _questionService = questionService;
        }

        public PartialViewResult Index(int id)
        {
            var questions = _questionService.GetListAll(x => x.ProductID == id);
            var answer = _tarzolDbContext.Answers.Where(İ => İ.ProductID == id).ToList();
            var product = _tarzolDbContext.Products.Where(i => i.ID == id).FirstOrDefault();
            var seller = _tarzolDbContext.Sellers.Where(i => i.ID == product.SellerID).FirstOrDefault();
            QuestionAndAnswerList questionAndAnswerList = new QuestionAndAnswerList()
            {
                Questions = questions,
                Answers = answer,
                Seller = seller
            };
            return PartialView(questionAndAnswerList);
        }

        public IActionResult AskQuestion(string userName,Question question)
        {
            var User = _tarzolDbContext.Users.Where(i => i.UserName == userName).FirstOrDefault();
            question.AppUserID = User.Id;
            question.Status = Core.Enums.Status.Active;
            question.CreatedDate = DateTime.Now;
            question.CreatedBy = User.UserName;
            _questionService.Add(question);
            return RedirectToAction("ViewProduct", "Product",new {id=question.ProductID });
        }
    }
}
