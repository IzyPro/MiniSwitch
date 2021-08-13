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
        public IActionResult Index()
        {
            var sourceNodes = _channelService.FetchAll();
            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Channel model)
        {
            var response = await _channelService.Edit(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Channel model)
        {
            var response = await _channelService.Create(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }
    }
}
