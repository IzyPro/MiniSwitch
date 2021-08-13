using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SourceNodeServices
{
    public interface ISourceNodeService
    {
        Task<ResponseManager> Create(SourceNode sourceNode);
        Task<ResponseManager> Edit(SourceNode sourceNode);
        Task<List<SourceNode>> FetchAll();
    }
}
