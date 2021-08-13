using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.SourceNodeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class SourceNodeController : Controller
    {
        private ISourceNodeService _sourceNodeService;

        public SourceNodeController(ISourceNodeService sourceNodeService)
        {
            _sourceNodeService = sourceNodeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sourceNodes = _sourceNodeService.FetchAll();
            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] SourceNode sourceNode)
        {
            var response = await _sourceNodeService.Edit(sourceNode);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SourceNode sourceNode)
        {
            var response = await _sourceNodeService.Create(sourceNode);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }
    }
}
