using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.RouteServices
{
    public interface IRouteService
    {
        Task<ResponseManager> Create(RouteViewModel model);
        Task<ResponseManager> Edit(RouteViewModel model);
        Task<List<Route>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
