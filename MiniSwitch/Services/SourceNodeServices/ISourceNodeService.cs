using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.SourceNodeServices
{
    public interface ISourceNodeService
    {
        Task<ResponseManager> Create(SourceNodeViewModel sourceNode);
        Task<ResponseManager> Edit(SourceNodeViewModel sourceNode);
        Task<PaginatedList<SourceNode>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
