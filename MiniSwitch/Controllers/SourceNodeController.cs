using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Helpers;
using MiniSwitch.Models;
using MiniSwitch.Services.SchemeServices;
using MiniSwitch.Services.SourceNodeServices;
using MiniSwitch.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class SourceNodeController : Controller
    {
        private ISourceNodeService _sourceNodeService;
        private ISchemeService _schemeService;

        public SourceNodeController(ISourceNodeService sourceNodeService, ISchemeService schemeService)
        {
            _sourceNodeService = sourceNodeService;
            _schemeService = schemeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var sourceNodes = await _sourceNodeService.FetchAll(searchString, pageNumber);
            ViewBag.Schemes = await _schemeService.FetchAll();

            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] SourceNodeViewModel sourceNode)
        {
            var response = await _sourceNodeService.Edit(sourceNode);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SourceNodeViewModel sourceNode)
        {
            var response = await _sourceNodeService.Create(sourceNode);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
