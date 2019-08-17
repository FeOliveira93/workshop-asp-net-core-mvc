using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMCV.Models;
using SalesWebMCV.Models.ViewModels;
using SalesWebMCV.Services;

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
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        //Deleta o Seller.
        public IActionResult Delete(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            
            if(obj is null)
            {
                return NotFound();
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
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj is null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}