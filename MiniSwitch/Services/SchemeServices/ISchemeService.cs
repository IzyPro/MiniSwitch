using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SchemeServices
{
    public interface ISchemeService
    {
        Task<ResponseManager> Create(Scheme model);
        Task<ResponseManager> Edit(Scheme model);
        Task<List<Scheme>> FetchAll();
    }
}
