using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Helpers;
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
            if (!IPAddressValidation.IsIPAddress(sinkNode.IpAddress))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Enter a valid IP Address"
                };
            }
            else if((int)sinkNode.Status < 1)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a valid status"
                };
            }

            var node = new SinkNode
            {
                Id = Guid.NewGuid(),
                Name = sinkNode.Name,
                HostName = sinkNode.HostName,
                IpAddress = sinkNode.IpAddress,
                Port = sinkNode.Port,
                Status = sinkNode.Status
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

            if (!IPAddressValidation.IsIPAddress(sinkNode.IpAddress))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Enter a valid IP Address"
                };
            }
            else if ((int)sinkNode.Status < 1)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a valid status"
                };
            }

            node.HostName = sinkNode.HostName;
            node.IpAddress = sinkNode.IpAddress;
            node.Name = sinkNode.Name;
            node.Port = sinkNode.Port;
            node.Status = sinkNode.Status;

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

        public async Task<PaginatedList<SinkNode>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.SinkNodes
                    .Where(s => s.Name.Contains(searchString) || s.HostName.Contains(searchString) || s.Port.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<SinkNode>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<SinkNode>.CreateAsync(_context.SinkNodes.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
