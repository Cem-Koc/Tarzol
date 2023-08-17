using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;


namespace Tarzol.WebUI.Controllers
{

    public class SellerController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        ISellerService _sellerService;
        IProductService _productService;
        INotificationService _notificationService;
        public SellerController(TarzolDbContext tarzolDbContext,ISellerService sellerService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IProductService productService, INotificationService notificationService)
        {
            _tarzolDbContext = tarzolDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _sellerService = sellerService;
            _productService = productService;
            _notificationService = notificationService;
        }

        
        public IActionResult Index(string userName)
        {
            var User = _tarzolDbContext.Users.Where(i => i.UserName == userName).FirstOrDefault();
            Seller seller = _tarzolDbContext.Sellers.Where(i => i.UserID == User.Id).FirstOrDefault();
            TempData["SellerID"] = seller.ID;
            if (seller.Status==Core.Enums.Status.UnApproved)
            {
                return RedirectToAction("UnApprovedSeller", "Seller");
            }

            return View(seller);
        }

        public IActionResult UnApprovedSeller()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SellerRegister(int UserId)
        {
            ViewBag.City = new SelectList(_tarzolDbContext.Cities, "ID", "CityName");
            ViewBag.AppUserId = UserId;
            return View();
        }

        public JsonResult LoadDistrict(int Id)
        {
            var district = _tarzolDbContext.Districts.Where(x => x.CityID == Id).ToList();
            return Json(new SelectList(district, "ID", "DistrictName"));
        }

        
        [HttpPost]
        public IActionResult SellerRegister(SellerImageModel sellerImageModel,IFormFile SellerImage)
        {
            Seller seller = new Seller();

            if (sellerImageModel.SellerImage!=null)
            {
            var extension = Path.GetExtension(sellerImageModel.SellerImage.FileName);
            var newimagename = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/SellerImage/", newimagename);
            var stream = new FileStream(location, FileMode.Create);
                sellerImageModel.SellerImage.CopyTo(stream);
                seller.ProfileImageUrl = newimagename;
            }

            seller.UserID = sellerImageModel.UserID;
            seller.CompanyName = sellerImageModel.CompanyName;
            seller.SellerAddress = sellerImageModel.SellerAddress;
            seller.TaxNumber = sellerImageModel.TaxNumber;
            seller.CityID = sellerImageModel.CityID;
            seller.DistrictID = sellerImageModel.DistrictID;




            seller.Status = Core.Enums.Status.UnApproved;
            seller.CreatedDate = DateTime.Now;
            

            if (_sellerService.Add(seller))
            {
                Notification notification = new Notification()
                {
                    CreatedBy = seller.CompanyName,
                    CreatedDate = DateTime.Now,
                    Status = Core.Enums.Status.Active,
                    CreativeID = seller.ID,
                    NotificationStatus = Core.Enums.NotificationStatus.NewSeller
                };
                _notificationService.Add(notification);
                _tarzolDbContext.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(seller);
           
        }

        [HttpGet]
        public IActionResult SellerAppRegister()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SellerAppRegister(SellerSignUpViewModel sellerSignUpViewModel)
        {
            AppUser appUser = new AppUser()
            {
                Email = sellerSignUpViewModel.Email,
                FirstName = sellerSignUpViewModel.FirstName,
                LastName = sellerSignUpViewModel.LastName,
                PhoneNumber = sellerSignUpViewModel.PhoneNumber,
                UserName = sellerSignUpViewModel.Email,
                Status = Core.Enums.Status.Active,
                CreatedDate = DateTime.Now,
                
                
            };
            if (sellerSignUpViewModel.Password == sellerSignUpViewModel.ComfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, sellerSignUpViewModel.Password);
                var result2 = await _userManager.AddToRoleAsync(appUser,"Seller");
                if (result.Succeeded && result2.Succeeded)
                {
                    return RedirectToAction("SellerRegister", "Seller", new { UserId=appUser.Id });
                   
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(sellerSignUpViewModel);
        }

       public PartialViewResult LeftMenu()
        {
            return PartialView();
        }

        ////////////////////////////////////

        [HttpGet]
        public IActionResult ProductAdd()
        {
            ViewBag.Kategori = new SelectList(_tarzolDbContext.Categories, "ID", "CategoryName");
            ViewBag.Color = new SelectList(_tarzolDbContext.ProductColors, "ID", "Color");
            ViewBag.Size = new SelectList(_tarzolDbContext.ProductSizes, "ID", "Size");
            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(ProductWithImageAdd p,string activename)
        {
            Product product = new Product();
            if (p.ImageOne != null)
            {
                var extension = Path.GetExtension(p.ImageOne.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageOne.CopyTo(stream);
                product.ImageOne = newimagename;

                if (p.ImageTwo != null)
                {
                    var extension2 = Path.GetExtension(p.ImageTwo.FileName);
                    var newimagename2 = Guid.NewGuid() + extension2;
                    var location2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename2);
                    var stream2 = new FileStream(location2, FileMode.Create);
                    p.ImageTwo.CopyTo(stream2);
                    product.ImageTwo = newimagename2;
                }
                if (p.ImageThree != null)
                {
                    var extension3 = Path.GetExtension(p.ImageThree.FileName);
                    var newimagename3 = Guid.NewGuid() + extension3;
                    var location3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename3);
                    var stream3 = new FileStream(location3, FileMode.Create);
                    p.ImageThree.CopyTo(stream3);
                    product.ImageThree = newimagename3;
                }
                if (p.ImageFour != null)
                {
                    var extension4 = Path.GetExtension(p.ImageFour.FileName);
                    var newimagename4 = Guid.NewGuid() + extension4;
                    var location4 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename4);
                    var stream4 = new FileStream(location4, FileMode.Create);
                    p.ImageFour.CopyTo(stream4);
                    product.ImageFour = newimagename4;
                }
            }

            product.ProductName = p.ProductName;
            product.MarketPrice = p.MarketPrice;
            product.UnitsInStock = p.UnitsInStock;
            product.DiscountedPrice = p.DiscountedPrice;
            product.TradeMark = p.TradeMark;
            product.ProductCode = p.ProductCode;
            product.CategoryID = p.CategoryID;
            product.SubCategoryID = p.SubCategoryID;
            product.ProductColorID = p.ProductColorID;
            product.ProductSizeID = p.ProductSizeID;
            product.Description = p.Description;
            product.ProductInformation = p.ProductInformation;
            product.Status = Core.Enums.Status.Active;
            product.SellerID =Convert.ToInt32(TempData["SellerID"]);
            product.CargoID = 1;
            _productService.Add(product);
            _tarzolDbContext.SaveChanges();
            var companyName = _tarzolDbContext.Sellers.Where(i => i.ID == product.SellerID).Select(i => i.CompanyName).FirstOrDefault();
            Notification notification = new Notification()
            {
                CreatedBy = companyName,
                CreatedDate = DateTime.Now,
                Status = Core.Enums.Status.Active,
                CreativeID = product.SellerID,
                CreativeInteractionID = product.ID,
                NotificationStatus = Core.Enums.NotificationStatus.NewProduct
            };
            _notificationService.Add(notification);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("Index", "Seller", new { userName = activename });
        }
        public JsonResult LoadSubCats(int Id)
        {
            List<SubCategory> sublist = new List<SubCategory>();
            var subcatss = _tarzolDbContext.CategoryAndSubCategories.Where(x => x.CategoryID == Id).Select(i => i.SubCategoryID).ToList();
            foreach (var item in subcatss)
            {
                var s = _tarzolDbContext.SubCategories.Where(i => i.ID == item).FirstOrDefault();
                sublist.Add(s);
            }
            return Json(new SelectList(sublist, "ID", "SubCategoryName"));
        }


        public IActionResult SellerProducts(string userName)
        {
            var User = _tarzolDbContext.Users.Where(i => i.UserName == userName).FirstOrDefault();
            var seller = _tarzolDbContext.Sellers.Where(i => i.UserID == User.Id).FirstOrDefault();
           
            var sellerProducts = _tarzolDbContext.Products.Where(i => i.SellerID == seller.ID).Where(x=>x.Status==Core.Enums.Status.Active).ToList();
           
            return View(sellerProducts);
        }

        public IActionResult PassiveProduct(int productId, string userName)
        {
            var product = _productService.GetBy(productId);
            product.Status = Core.Enums.Status.Passive;
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("SellerProducts", new { userName = userName });
        }
        public IActionResult PassiveProductList(string userName)
        {
            var User = _tarzolDbContext.Users.Where(i => i.UserName == userName).FirstOrDefault();
            var seller = _tarzolDbContext.Sellers.Where(i => i.UserID == User.Id).FirstOrDefault();
            var sellerPassiveProducts = _tarzolDbContext.Products.Where(i => i.SellerID == seller.ID).Where(x => x.Status == Core.Enums.Status.Passive).ToList();
            return View(sellerPassiveProducts);
        }

        [HttpGet]
        public IActionResult UpdateProduct(int productId, string userName)
        {
            ViewBag.Kategori = new SelectList(_tarzolDbContext.Categories, "ID", "CategoryName");
            ViewBag.Color = new SelectList(_tarzolDbContext.ProductColors, "ID", "Color");
            ViewBag.Size = new SelectList(_tarzolDbContext.ProductSizes, "ID", "Size");
            var product = _productService.GetBy(productId);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product, string userName)
        {
            product.ModifiedBy = userName;
            product.ModifiedDate = DateTime.Now;
            product.Status = Core.Enums.Status.Active;
            if (_productService.Update(product))
            {
                return RedirectToAction("SellerProducts", new { userName = userName });
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult SellerAnswer(string userName)
        {
            var User = _tarzolDbContext.Users.Where(i => i.UserName == userName).FirstOrDefault();
            Seller seller = _tarzolDbContext.Sellers.Where(i => i.UserID == User.Id).FirstOrDefault();

            ViewBag.id = null;

            ProductInQuestionModel productInQuestionModel = new ProductInQuestionModel()
            {               
                
                Products = _tarzolDbContext.Products.Where(i=>i.SellerID==seller.ID).ToList(),
                Questions = _tarzolDbContext.Questions.ToList(),
                Answers=_tarzolDbContext.Answers.ToList()
            };
            return View(productInQuestionModel);
        }

       [HttpGet]
        public IActionResult ProductQuestion(int productId)
        {
            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            Seller seller = _tarzolDbContext.Sellers.Where(i => i.UserID == sellerUser.Id).FirstOrDefault();

            ProductInQuestionModel productInQuestionModel = new ProductInQuestionModel()
            {
                Products = _tarzolDbContext.Products.Where(i => i.ID == productId).ToList(),
                Questions = _tarzolDbContext.Questions.Where(i => i.ProductID == productId).ToList(),
                Answers = _tarzolDbContext.Answers.Where(i => i.ProductID == productId).ToList()
            };
            return View(productInQuestionModel);
        }

        [HttpGet]
        public IActionResult ProductAnswer(int productId, int questionId)
        {
            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            Seller seller = _tarzolDbContext.Sellers.Where(i => i.UserID == sellerUser.Id).FirstOrDefault();

            AnswerOfTheQuestionModel answerOfTheQuestion = new AnswerOfTheQuestionModel()
            {
                Question=_tarzolDbContext.Questions.Where(i=>i.ID==questionId).FirstOrDefault(),
            };
            return View(answerOfTheQuestion);
        }
        [HttpPost]
        public IActionResult ProductAnswer(AnswerOfTheQuestionModel answerOfTheQuestionModel,int productId, int questionId)
        {
            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            Seller seller = _tarzolDbContext.Sellers.Where(i => i.UserID == sellerUser.Id).FirstOrDefault();

            Answer answer = new Answer()
            {
                CreatedDate = DateTime.Now,
                Status = Core.Enums.Status.Active,
                AnswerContent = answerOfTheQuestionModel.Answer.AnswerContent,
                QuestionID = questionId,
                ProductID = productId,
                SellerID = seller.ID
            };

            _tarzolDbContext.Answers.Add(answer);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("SellerAnswer",new { userName=User.Identity.Name });
        }

      [HttpGet]
      public IActionResult ActiveOrder()
        {
            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var orderDetails = _tarzolDbContext.OrderDetails.Where(i=>i.WasDelivered==false).ToList();
            var sellerProducts = _productService.GetListAll(i => i.SellerID == sellerUser.Id);

            ActiveOrderModel activeOrderModel = new ActiveOrderModel()
            {
                OrderDetails = orderDetails,
                Products = sellerProducts
            };
            return View(activeOrderModel);
        }

        [HttpPost]
        public IActionResult ActiveOrder(IFormCollection values,int productID,int orderID,string returnUrl,int deliveredDateCounter)
        {
            
            var delivreyDate = Convert.ToDateTime(values[deliveredDateCounter.ToString()]);
           
            

            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var orderDetail = _tarzolDbContext.OrderDetails.Where(i => i.ProductID == productID).Where(x => x.OrderID == orderID).FirstOrDefault();
            orderDetail.WasDelivered = true;
            orderDetail.ModifiedDate = delivreyDate;
            orderDetail.ModifiedBy = sellerUser.UserName;
            _tarzolDbContext.SaveChanges();

            var selectedorder = _tarzolDbContext.OrderDetails.Where(i => i.OrderID == orderID).ToList();
            int counter = 0;
            foreach (var item in selectedorder)
            {
                if (item.WasDelivered == true)
                {
                    counter++;
                }
            }
            if (selectedorder.Count() == counter)
            {
                var order = _tarzolDbContext.Orders.Where(i => i.ID == orderID).FirstOrDefault();
                order.DelivreyDate = delivreyDate;
                order.ModifiedBy = sellerUser.UserName;
                order.ModifiedDate = DateTime.Now;
                _tarzolDbContext.SaveChanges();

            }
            return Redirect(returnUrl);
        }
        
        public IActionResult DeliveredOrder()
        {
            var sellerUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
           
            var sellerId = _tarzolDbContext.Sellers.Where(i=>i.UserID==sellerUser.Id).Select(x=>x.UserID).FirstOrDefault();
            var orderDetails = _tarzolDbContext.OrderDetails.Where(i => i.ID == sellerId).Where(x=>x.WasDelivered==true).ToList();
            return View(orderDetails);
        }

        public IActionResult SellerPage(int sellerId)
        {
            var seller = _sellerService.GetBy(sellerId);
            var products = _productService.GetListAll(i => i.SellerID == sellerId);
            var orderDetailsList = _tarzolDbContext.OrderDetails.ToList();
            List<OrderDetail> orderDetails=new List<OrderDetail>();
            foreach (var item in products)
            {
                if (_tarzolDbContext.OrderDetails.Any(i=>i.ProductID==item.ID))
                {
                    foreach (var orderdetail in orderDetailsList)
                    {
                        if (orderdetail.ProductID == item.ID)
                        {
                            orderDetails.Add(orderdetail);
                        }
                    }
                }
            }
            var bestOrderID = orderDetails.Select(i => i.ProductID).Distinct().Take(3).ToList();
            List<Product> productList = new List<Product>();
            foreach (var item in bestOrderID)
            {
                var product = _tarzolDbContext.Products.Where(i => i.ID == item).FirstOrDefault();
                productList.Add(product);
            }
            if (productList.Count()<3)
            {
                foreach (var item in products)
                {
                    if (productList.Where(i=>i.ID==item.ID).Count()==0 && productList.Count() < 3)
                    {
                        productList.Add(item);
                    }
                }
            }

            SellerPageModel sellerPageModel = new SellerPageModel()
            {
                Seller = seller,
                Products= products,
                 BestProducts= productList,
            };
            return View(sellerPageModel);
        }

    }
}

