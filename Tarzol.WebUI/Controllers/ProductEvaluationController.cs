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
    public class ProductEvaluationController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IProductService _productService;
        IProductRatingService _productRatingService;
        ICommentService _commentService;
        INotificationService _notificationService;
        public ProductEvaluationController(TarzolDbContext tarzolDbContext, IProductService productService, IProductRatingService productRatingService, ICommentService commentService, INotificationService notificationService)
        {
            _tarzolDbContext = tarzolDbContext;
            _productService = productService;
            _productRatingService = productRatingService;
            _commentService = commentService;
            _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductEvaluation(int orderID,int productID)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var orderDetail = _tarzolDbContext.OrderDetails.Where(i => i.ProductID == productID).Where(x => x.OrderID == orderID).FirstOrDefault();
            ProductEvaluationModel productEvaluationModel = new ProductEvaluationModel()
            {
                OrderDetail = orderDetail,
            };
            var comment=_tarzolDbContext.Comments.Where(i => i.ProductID == productID).Where(x => x.OrderID == orderID).Where(y => y.AppUserID == user.Id).FirstOrDefault();
            if (comment!=null)
            {
                productEvaluationModel.CommentContext = comment.CommentContext;
            }
            var rating = _tarzolDbContext.ProductRatings.Where(i => i.ProductID == productID).Where(x => x.OrderID == orderID).Where(y => y.AppUserID == user.Id).FirstOrDefault();
            if (rating!=null)
            {
                productEvaluationModel.ProductRatingPoint = rating.ProductRatingPoint;
            }
            return View(productEvaluationModel);
        }
        [HttpPost]
        public IActionResult ProductEvaluation(ProductEvaluationModel productEvaluationModel)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            if (productEvaluationModel.CommentContext!=null)
            {
                Comment comment = new Comment()
                {
                    OrderID = productEvaluationModel.OrderID,
                    ProductID = productEvaluationModel.ProductID,
                    AppUserID = user.Id,
                    CommentContext = productEvaluationModel.CommentContext,
                    Status = Core.Enums.Status.Active,
                    CreatedBy = user.UserName,
                    CreatedDate = DateTime.Now
                };
                _commentService.Add(comment);

            }

            if (productEvaluationModel.ProductRatingPoint!=0)
            {
                ProductRating productRating = new ProductRating()
                {
                    OrderID = productEvaluationModel.OrderID,
                    ProductID = productEvaluationModel.ProductID,
                    AppUserID = user.Id,
                    ProductRatingPoint = productEvaluationModel.ProductRatingPoint,
                    Status = Core.Enums.Status.Active,
                    CreatedBy = user.UserName,
                    CreatedDate = DateTime.Now
                };
                _productRatingService.Add(productRating);

                Notification notification = new Notification()
                {
                    CreatedBy = productRating.AppUser.FirstName +" "+ productRating.AppUser.LastName,
                    CreatedDate = DateTime.Now,
                    Status = Core.Enums.Status.Active,
                    CreativeID = Convert.ToInt32(productRating.AppUserID),
                    CreativeInteractionID = productRating.ID,
                    NotificationStatus = Core.Enums.NotificationStatus.NegativeNotice
                };
                _notificationService.Add(notification);
                _tarzolDbContext.SaveChanges();
            }
           

            return Redirect(productEvaluationModel.returnUrl);
        }
    }
}
