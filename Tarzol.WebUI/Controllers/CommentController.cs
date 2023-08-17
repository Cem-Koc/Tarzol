using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Controllers
{
    public class CommentController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        ICommentService _commentService;

        public CommentController(TarzolDbContext tarzolDbContext, ICommentService commentService)
        {
            _tarzolDbContext = tarzolDbContext;
            _commentService = commentService;
        }

        public PartialViewResult CommentListByProduct(int id)
        {
            var values = _commentService.GetListAll(x=>x.ProductID==id);
            return PartialView(values);
        }
    }
}
