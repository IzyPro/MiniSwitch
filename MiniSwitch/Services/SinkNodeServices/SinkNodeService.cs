using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SinkNodeServices
{
    public class SinkNodeService : ISinkNodeService
    {
        private MiniSwitchContext _context;

        public SinkNodeService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(SinkNode sinkNode)
        {
            var node = new SinkNode
            {
                Id = Guid.NewGuid(),
                Name = sinkNode.Name,
                HostName = sinkNode.HostName,
                IpAddress = sinkNode.IpAddress,
                Port = sinkNode.Port,
            };

            await _context.SinkNodes.AddAsync(node);
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

        public async Task<ResponseManager> Edit(SinkNode sinkNode)
        {
            var node = _context.SinkNodes.Where(x => x.Id == sinkNode.Id).FirstOrDefault();
            if (node == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No sink node with Id {sinkNode.Id} found"
                };
            node = sinkNode;
            _context.SinkNodes.Update(node);
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

        public async Task<List<SinkNode>> FetchAll()
        {
            return await _context.SinkNodes.ToListAsync();
        }
    }
}
