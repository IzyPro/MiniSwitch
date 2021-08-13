using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniSwitch.Models;

namespace MiniSwitch.Services.ChannelServices
{
    public interface IChannelService
    {
        Task<ResponseManager> Create(Channel model);
        Task<ResponseManager> Edit(Channel model);
        Task<List<Channel>> FetchAll();
    }
}
