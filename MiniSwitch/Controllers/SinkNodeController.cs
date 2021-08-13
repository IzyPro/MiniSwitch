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
        public IActionResult Index()
        {
            var sourceNodes = _sinkNodeService.FetchAll();
            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] SinkNode sourceNode)
        {
            var response = await _sinkNodeService.Edit(sourceNode);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SinkNode sourceNode)
        {
            var response = await _sinkNodeService.Create(sourceNode);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }
    }
}
