using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.TransactionsServices
{
    public interface ITransactionService
    {
        Task<ResponseManager> Create(TransactionViewModel model);
        Task<List<Transaction>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
