using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers { 
    public class SellersController : Controller {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) {
            _sellerService = sellerService;
        }

        public IActionResult Index()  {

            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]//do this becasue it is an action of post and not get
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {
            //insert record on database
            _sellerService.Insert(seller);

            //return page index
            return RedirectToAction(nameof(Index));

        }
    }
}