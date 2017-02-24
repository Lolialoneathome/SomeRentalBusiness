using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class BikeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string adress)
        {
            BikeViewModel bikeVm = new BikeViewModel(adress);
            return View(bikeVm);
        }

        [HttpPost]
        public IActionResult Create(BikeViewModel vm)
        {
            return View();
        }
    }
}
