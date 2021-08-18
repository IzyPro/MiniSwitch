using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.RouteServices;
using MiniSwitch.Services.SinkNodeServices;
using MiniSwitch.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class RouteController : Controller
    {
        private IRouteService _routeService;
        private ISinkNodeService _sinkNodeService;

        public RouteController(IRouteService routeService, ISinkNodeService sinkNodeService)
        {
            _routeService = routeService;
            _sinkNodeService = sinkNodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var routes = await _routeService.FetchAll(searchString, pageNumber);
            var sinkNodes = await _sinkNodeService.FetchAll();
            ViewBag.SinkNodes = sinkNodes.Where(x => x.Status == Enums.NodeStatusEnum.Active).ToList();
            return View(routes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] RouteViewModel model)
        {
            var response = await _routeService.Edit(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RouteViewModel model)
        {
            var response = await _routeService.Create(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
