using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.FeeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class FeeController : Controller
    {
        private IFeeService _feeService;

        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sourceNodes = _feeService.FetchAll();
            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Fee model)
        {
            var response = await _feeService.Edit(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Fee model)
        {
            var response = await _feeService.Create(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }
    }
}
