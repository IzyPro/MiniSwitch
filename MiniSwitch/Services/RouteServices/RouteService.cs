using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        private MiniSwitchContext _context;

        public RouteService(MiniSwitchContext context)
        {
            _context = context;
        }


        public async Task<ResponseManager> Create(RouteViewModel model)
        {
            var sinknode = await _context.SinkNodes.Where(x => x.Id == model.SinkNodeID).FirstOrDefaultAsync();

            if (sinknode == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a sink node"
                };

            else if(model.BIN.Length != 6)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Enter a valid BIN"
                };

            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                BIN = model.BIN,
                Description = model.Description,
                SinkNode = sinknode,
            };

            await _context.Routes.AddAsync(route);
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

        public async Task<ResponseManager> Edit(RouteViewModel model)
        {
            var sinknode = await _context.SinkNodes.Where(x => x.Id == model.SinkNodeID).FirstOrDefaultAsync();

            if (sinknode == null)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a sink node"
                };
            }

            var route = _context.Routes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (route == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Route with Id {model.Id} found"
                };

            route.Name = model.Name;
            route.SinkNode = sinknode;
            route.BIN = model.BIN;
            route.Description = model.Description;

            _context.Routes.Update(route);
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

        public async Task<List<Route>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.Routes
                    .Include(s => s.SinkNode)
                    .Where(s => s.Name.Contains(searchString) || s.SinkNode.Name.Contains(searchString) || s.Description.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<Route>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<Route>.CreateAsync(_context.Routes.Include(s => s.SinkNode).AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
