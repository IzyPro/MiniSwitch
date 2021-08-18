using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.TransactionTypeServices
{
    public interface ITransactionTypeService
    {
        Task<ResponseManager> Create(TransactionType model);
        Task<ResponseManager> Edit(TransactionType model);
        Task<List<TransactionType>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
