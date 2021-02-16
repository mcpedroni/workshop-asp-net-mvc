 using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewsModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;

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

        public IActionResult Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]//do this becasue it is an action of delete and not get
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id) {
            _sellerService.Remove(Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }

            return View(obj); 
        }

        public IActionResult Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) {

            //it is difficult to happening, but if id is not the same...we can not edit
            if (id !=  seller.Id) {
                return BadRequest();
            }

            try {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            } catch (NotFoundException) {
                return NotFound();
            } catch (DbConcurrencyException) {
                return BadRequest();
            }            
        }
    }
}