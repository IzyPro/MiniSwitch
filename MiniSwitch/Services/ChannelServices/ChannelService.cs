using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.ChannelServices
{
    public class ChannelService : IChannelService
    {
        private MiniSwitchContext _context;

        public ChannelService(MiniSwitchContext context)
        {
            _context = context;
        }


        public async Task<ResponseManager> Create(Channel model)
        {
            var channel = new Channel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Code = model.Code,
            };

            await _context.Channels.AddAsync(channel);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ResponseManager
                {
                    isSuccess = true,
                    Message = "Added Successfully"
                };
            }
            else
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Failed to Add"
                };
            }
        }

        public async Task<ResponseManager> Edit(Channel model)
        {
            var channel = _context.Channels.Where(x => x.Id == model.Id).FirstOrDefault();
            if (channel == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No sink node with Id {model.Id} found"
                };
            channel = model;
            _context.Channels.Update(channel);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ResponseManager
                {
                    isSuccess = true,
                    Message = "Updated Successfully"
                };
            }
            else
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Update Failed"
                };
            }
        }

        public async Task<List<Channel>> FetchAll()
        {
            return await _context.Channels.ToListAsync();
        }
    }
}
