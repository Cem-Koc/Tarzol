using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.ViewComponents.Comment
{
    public class CommentList:ViewComponent
    {
        
        TarzolDbContext _tarzolDbContext;

        public CommentList(TarzolDbContext tarzolDbContext)
        {
            
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.ProductRatings = _tarzolDbContext.ProductRatings.Where(i=>i.ProductID==id).ToList();          
            var values = _tarzolDbContext.Comments.Include("AppUser").Where(i => i.ProductID == id).ToList();
            return View(values);
        }
    }
}
