using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.SchemeServices
{
    public class SchemeService : ISchemeService
    {
        private MiniSwitchContext _context;

        public SchemeService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(Scheme model)
        {
            var scheme = new Scheme
            {
                Id = Guid.NewGuid(),
                Channel = model.Channel,
                Description = model.Description,
                Fee = model.Fee,
                Route = model.Route,
                TransactionType = model.TransactionType,
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

        public async Task<ResponseManager> Edit(Scheme model)
        {
            var scheme = _context.Schemes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (scheme == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No Scheme with Id {model.Id} found"
                };
            scheme = model;
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

        public async Task<List<Scheme>> FetchAll()
        {
            return await _context.Schemes.ToListAsync();
        }
    }
}
