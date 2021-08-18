using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Helpers;
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
            if (!IPAddressValidation.IsIPAddress(sourceNode.IpAddress))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Enter a valid IP Address"
                };
            }
            else if ((int)sourceNode.Status < 1)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a valid status"
                };
            }

            var node = new SourceNode
            {
                Id = Guid.NewGuid(),
                Name = sourceNode.Name,
                HostName = sourceNode.HostName,
                IpAddress = sourceNode.IpAddress,
                Port = sourceNode.Port,
                Scheme = sourceNode.Scheme,
                Status = sourceNode.Status
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

            if (!IPAddressValidation.IsIPAddress(sourceNode.IpAddress))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Enter a valid IP Address"
                };
            }
            else if ((int)sourceNode.Status < 1)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a valid status"
                };
            }

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

        public async Task<PaginatedList<SourceNode>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.SourceNodes
                    .Where(s =>
                        s.Name.Contains(searchString) ||
                        s.IpAddress.Contains(searchString) ||
                        s.Port.Contains(searchString) ||
                        s.Status.ToString().Contains(searchString) ||
                        s.Id.ToString().Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<SourceNode>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<SourceNode>.CreateAsync(_context.SourceNodes.AsNoTracking(), pageNumber ?? 1, pageSize);
            //return await _context.SourceNodes.ToListAsync();
        }
    }
}
