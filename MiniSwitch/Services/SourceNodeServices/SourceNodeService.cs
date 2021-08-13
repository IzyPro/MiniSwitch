using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SourceNodeServices
{
    public class SourceNodeService : ISourceNodeService
    {
        private MiniSwitchContext _context;

        public SourceNodeService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(SourceNode sourceNode)
        {
            var node = new SourceNode
            {
                Id = Guid.NewGuid(),
                Name = sourceNode.Name,
                HostName = sourceNode.HostName,
                IpAddress = sourceNode.IpAddress,
                Port = sourceNode.Port,
                Scheme = sourceNode.Scheme,
            };

            await _context.SourceNodes.AddAsync(node);
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

        public async Task<ResponseManager> Edit(SourceNode sourceNode)
        {
            var node = _context.SourceNodes.Where(x => x.Id == sourceNode.Id).FirstOrDefault();
            if (node == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Source node with Id {sourceNode.Id} found"
                };
            node = sourceNode;
            _context.SourceNodes.Update(node);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
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

        public async Task<List<SourceNode>> FetchAll()
        {
            return await _context.SourceNodes.ToListAsync();
        }
    }
}
