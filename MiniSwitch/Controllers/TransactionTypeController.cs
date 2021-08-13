using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Models;
using MiniSwitch.Services.TransactionTypeServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class TransactionTypeController : Controller
    {
        private ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sourceNodes = _transactionTypeService.FetchAll();
            return View(sourceNodes);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] TransactionType model)
        {
            var response = await _transactionTypeService.Edit(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TransactionType model)
        {
            var response = await _transactionTypeService.Create(model);
            if (response.isSuccess)
                ViewBag.Success = response.Message;
            else
                ViewBag.Error = response.Message;
            return RedirectToAction("Index");
        }
    }
}
