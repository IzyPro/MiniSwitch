using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.RouteServices
{
    public interface IRouteService
    {
        Task<ResponseManager> Create(Route model);
        Task<ResponseManager> Edit(Route model);
        Task<List<Route>> FetchAll();
    }
}
