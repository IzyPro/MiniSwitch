using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.SchemeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class SchemeController : Controller
    {
        private ISchemeService _schemeService;

        public SchemeController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var schemes = _schemeService.FetchAll();
            return View(schemes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Scheme model)
        {
            var response = await _schemeService.Edit(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Scheme model)
        {
            var response = await _schemeService.Create(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
