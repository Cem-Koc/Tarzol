using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SellerController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        ISellerService _sellerService;

        public SellerController(TarzolDbContext tarzolDbContext, ISellerService sellerService)
        {
            _tarzolDbContext = tarzolDbContext;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var seller = _sellerService.GetListAll();
            return View(seller);
        }

        public IActionResult ActiveSeller()
        {
            var activeSeller = _sellerService.GetListAll(i => i.Status == Core.Enums.Status.Active);
            return View(activeSeller);
        }
        public IActionResult SellerActiveAndPassive(int sellerId,string returnUrl)
        {
            var seller = _sellerService.GetBy(sellerId);
            if (seller.Status == Core.Enums.Status.Active)
            {
                seller.Status = Core.Enums.Status.Passive;
            }
            else
            {
                seller.Status = Core.Enums.Status.Active;
            }

            _sellerService.Update(seller);
            return Redirect(returnUrl);
        }

        public IActionResult PassiveSeller()
        {
            var passiveSeller = _sellerService.GetListAll(i => i.Status == Core.Enums.Status.Passive);
            return View(passiveSeller);
        }

        public IActionResult UnapprovedSeller()
        {
            var unapprovedSeller = _sellerService.GetListAll(i => i.Status == Core.Enums.Status.UnApproved);
            return View(unapprovedSeller);
        }
        public IActionResult Approval(int sellerId)
        {
            var seller = _sellerService.GetBy(sellerId);
            seller.Status = Core.Enums.Status.Active;
            _sellerService.Update(seller);
            return RedirectToAction("UnapprovedSeller");
        }

        public IActionResult SellerDetail(int sellerId)
        {
            var seller = _sellerService.GetBy(sellerId);
            var sellerCity = _tarzolDbContext.Cities.Where(i => i.ID == seller.CityID).FirstOrDefault();
            var sellerDistrict = _tarzolDbContext.Districts.Where(i => i.ID == seller.DistrictID).FirstOrDefault();
            ViewBag.sellerCity = sellerCity.CityName;
            ViewBag.sellerDistrict = sellerDistrict.DistrictName;
            return View(seller);
        }
    }
}
