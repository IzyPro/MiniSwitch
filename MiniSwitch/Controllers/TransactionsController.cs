using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.Services.SchemeServices;
using MiniSwitch.Services.SourceNodeServices;
using MiniSwitch.Services.TransactionsServices;
using MiniSwitch.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    public class TransactionsController : Controller
    {
        private ITransactionService _transactionService;
        private ISourceNodeService _sourceNodeService;

        public TransactionsController(ITransactionService transactionService, ISourceNodeService sourceNodeService)
        {
            _transactionService = transactionService;
            _sourceNodeService = sourceNodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString = "", int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var transactions = await _transactionService.FetchAll(searchString, pageNumber);
            var sourceNodes = await _sourceNodeService.FetchAll();

            ViewBag.SourceNodes = sourceNodes.Where(s => s.Status == Enums.NodeStatusEnum.Active).ToList();

            return View(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TransactionViewModel model)
        {
            var response = await _transactionService.Create(model);
            if (response.isSuccess)
                TempData["Success"] = response.Message;
            else
                TempData["Error"] = response.Message;
            return RedirectToAction("Index");
        }
    }
}
