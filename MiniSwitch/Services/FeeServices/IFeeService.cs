using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.FeeServices
{
    public interface IFeeService
    {
        Task<ResponseManager> Create(Fee model);
        Task<ResponseManager> Edit(Fee model);
        Task<List<Fee>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
