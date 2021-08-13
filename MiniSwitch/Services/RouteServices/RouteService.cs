using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.RouteServices
{
    public class RouteService : IRouteService
    {
        private MiniSwitchContext _context;

        public RouteService(MiniSwitchContext context)
        {
            _context = context;
        }


        public async Task<ResponseManager> Create(Route model)
        {
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CardPAN = model.CardPAN,
                Description = model.Description,
                SinkNode = model.SinkNode,
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

        public async Task<ResponseManager> Edit(Route model)
        {
            var route = _context.Routes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (route == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Route with Id {model.Id} found"
                };
            route = model;
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

        public async Task<List<Route>> FetchAll()
        {
            return await _context.Routes.ToListAsync();
        }
    }
}
