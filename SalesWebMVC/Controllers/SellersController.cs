using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewsModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()  {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create() {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments }; //return departments
            return View(viewModel);
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