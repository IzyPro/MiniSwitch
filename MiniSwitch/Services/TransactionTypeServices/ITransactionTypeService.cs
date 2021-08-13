using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.TransactionTypeServices
{
    public interface ITransactionTypeService
    {
        Task<ResponseManager> Create(TransactionType sourceNode);
        Task<ResponseManager> Edit(TransactionType sourceNode);
        Task<List<TransactionType>> FetchAll();
    }
}
