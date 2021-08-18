using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.SinkNodeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class SinkNodeController : Controller
    {
        private ISinkNodeService _sinkNodeService;

        public SinkNodeController(ISinkNodeService sinkNodeService)
        {
            _sinkNodeService = sinkNodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var sinkNodes = await _sinkNodeService.FetchAll(searchString, pageNumber);

            return View(sinkNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] SinkNode node)
        {
            var response = await _sinkNodeService.Edit(node);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SinkNode node)
        {
            var response = await _sinkNodeService.Create(node);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
