using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SinkNodeServices
{
    public interface ISinkNodeService
    {
        Task<ResponseManager> Create(SinkNode sourceNode);
        Task<ResponseManager> Edit(SinkNode sourceNode);
        Task<PaginatedList<SinkNode>> FetchAll(string searchString = "", int? pageNumber = 1);
    }
}
