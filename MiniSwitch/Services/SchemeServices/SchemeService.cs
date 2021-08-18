using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.SchemeServices
{
    public class SchemeService : ISchemeService
    {
        private MiniSwitchContext _context;

        public SchemeService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(SchemeViewModel model)
        {
            var transactionType = await _context.TransactionTypes.Where(x => x.Id == model.TransactionTypeID).FirstOrDefaultAsync();
            var channel = await _context.Channels.Where(x => x.Id == model.ChannelID).FirstOrDefaultAsync();
            var route = await _context.Routes.Where(x => x.Id == model.RouteID).FirstOrDefaultAsync();
            var fee = await _context.Fees.Where(x => x.Id == model.FeeID).FirstOrDefaultAsync();

            var scheme = new Scheme
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Channel = channel,
                Description = model.Description,
                Fee = fee,
                Route = route,
                TransactionType = transactionType,
            };

            await _context.Schemes.AddAsync(scheme);
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

        public async Task<ResponseManager> Edit(SchemeViewModel model)
        {
            var transactionType = await _context.TransactionTypes.Where(x => x.Id == model.TransactionTypeID).FirstOrDefaultAsync();
            var channel = await _context.Channels.Where(x => x.Id == model.ChannelID).FirstOrDefaultAsync();
            var route = await _context.Routes.Where(x => x.Id == model.RouteID).FirstOrDefaultAsync();
            var fee = await _context.Fees.Where(x => x.Id == model.FeeID).FirstOrDefaultAsync();


            var scheme = _context.Schemes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (scheme == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Scheme with Id {model.Id} found"
                };

            scheme.Channel = channel;
            scheme.TransactionType = transactionType;
            scheme.Channel = channel;
            scheme.Fee = fee;
            scheme.Name = model.Name;
            scheme.Description = model.Description;

            _context.Schemes.Update(scheme);
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

        public async Task<List<Scheme>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.Schemes
                    .Include(r => r.Route)
                    .Include(t => t.TransactionType)
                    .Include(c => c.Channel)
                    .Include(f => f.Fee)
                    .Where(s => s.Name.Contains(searchString) || s.TransactionType.Name.Contains(searchString) || s.Fee.Name.Contains(searchString) || s.Channel.Name.Contains(searchString) || s.Route.Name.Contains(searchString) || s.Description.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<Scheme>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<Scheme>.CreateAsync(
                _context.Schemes
                    .Include(r => r.Route)
                    .Include(t => t.TransactionType)
                    .Include(c => c.Channel)
                    .Include(f => f.Fee)
                    .AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
