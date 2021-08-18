using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Helpers;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.SourceNodeServices
{
    public class SourceNodeService : ISourceNodeService
    {
        private MiniSwitchContext _context;

        public SourceNodeService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(SourceNodeViewModel sourceNode)
        {
            var scheme = await _context.Schemes.Where(x => x.Id == sourceNode.SchemeID).FirstOrDefaultAsync();
            if (scheme == null)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a scheme"
                };
            }
            else if (!IPAddressValidation.IsIPAddress(sourceNode.IpAddress))
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
                Scheme = scheme,
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

        public async Task<ResponseManager> Edit(SourceNodeViewModel sourceNode)
        {
            var scheme = await _context.Schemes.Where(x => x.Id == sourceNode.SchemeID).FirstOrDefaultAsync();
            if (scheme == null)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a scheme"
                };
            }
            else if (!IPAddressValidation.IsIPAddress(sourceNode.IpAddress))
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
            var node = _context.SourceNodes.Where(x => x.Id == sourceNode.Id).FirstOrDefault();
            if (node == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Source node with Id {sourceNode.Id} found"
                };
            node.Scheme = scheme;
            node.Name = sourceNode.Name;
            node.HostName = sourceNode.HostName;
            node.IpAddress = sourceNode.IpAddress;
            node.Port = sourceNode.Port;
            node.Status = sourceNode.Status;

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
                    .Include(s => s.Scheme)
                    .Where(s => s.Name.Contains(searchString) || s.Scheme.Name.Contains(searchString) || s.IpAddress.Contains(searchString) || s.Port.Contains(searchString) || s.HostName.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<SourceNode>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<SourceNode>.CreateAsync(_context.SourceNodes.Include(s => s.Scheme).AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
