using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.ChannelServices;
using MiniSwitch.Services.FeeServices;
using MiniSwitch.Services.RouteServices;
using MiniSwitch.Services.SchemeServices;
using MiniSwitch.Services.TransactionTypeServices;
using MiniSwitch.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class SchemeController : Controller
    {
        private ISchemeService _schemeService;
        private IRouteService _routeService;
        private ITransactionTypeService _transactionTypeService;
        private IFeeService _feeService;
        private IChannelService _channelService;

        public SchemeController(ISchemeService schemeService, IRouteService routeService, ITransactionTypeService transactionTypeService, IFeeService feeService, IChannelService channelService)
        {
            _schemeService = schemeService;

            _routeService = routeService;
            _transactionTypeService = transactionTypeService;
            _channelService = channelService;
            _feeService = feeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var scheme = await _schemeService.FetchAll(searchString, pageNumber);

            ViewBag.Routes = await _routeService.FetchAll();
            ViewBag.TransactionTypes = await _transactionTypeService.FetchAll();
            ViewBag.Channels = await _channelService.FetchAll();
            ViewBag.Fees = await _feeService.FetchAll();


            return View(scheme);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] SchemeViewModel model)
        {
            var response = await _schemeService.Edit(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SchemeViewModel model)
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
