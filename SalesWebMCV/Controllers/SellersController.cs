using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMCV.Models;
using SalesWebMCV.Models.ViewModels;
using SalesWebMCV.Services;
using SalesWebMCV.Services.Exceptions;

namespace SalesWebMCV.Controllers
{
    public class SellersController : Controller
    {
        private readonly DepartmentsService _departmentsService;
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService, DepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
            _sellerService = sellerService;
        }

        //Controller que retorna o principal de Seller
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Controller que retorna o conteúdo de uma página de criação
        public IActionResult Create()
        {
            //Retorna os departamentos para inserí-los na lista.
            var departments = _departmentsService.FindAll();
            var ViewModel = new SellerFormViewModel() { Departments = departments };
            return View(ViewModel);
        }

        //Recebe um POST para criar Seller.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var department = _departmentsService.FindAll();
                var viewMOdel = new SellerFormViewModel { Departments = department };
                return View(viewMOdel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //Deleta o Seller.
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided." });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        //Recebe post com delete.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentsService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel() { Departments = departments, Seller = obj };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var department = _departmentsService.FindAll();
                var viewMOdel = new SellerFormViewModel { Departments = department };
                return View(viewMOdel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}