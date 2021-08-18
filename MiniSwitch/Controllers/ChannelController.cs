using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.ChannelServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class ChannelController : Controller
    {
        private IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var channels = await _channelService.FetchAll(searchString, pageNumber);
            return View(channels);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Channel model)
        {
            var response = await _channelService.Edit(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Channel model)
        {
            var response = await _channelService.Create(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
