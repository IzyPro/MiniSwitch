using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.SchemeServices
{
    public interface ISchemeService
    {
        Task<ResponseManager> Create(SchemeViewModel model);
        Task<ResponseManager> Edit(SchemeViewModel model);
        Task<List<Scheme>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
