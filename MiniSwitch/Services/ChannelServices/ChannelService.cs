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

            channel.Name = model.Name;
            channel.Code = model.Code;
            channel.Description = model.Description;

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

        public async Task<List<Channel>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.Channels
                    .Where(s => s.Name.Contains(searchString) || s.Code.Contains(searchString) || s.Description.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<Channel>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<Channel>.CreateAsync(_context.Channels.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
