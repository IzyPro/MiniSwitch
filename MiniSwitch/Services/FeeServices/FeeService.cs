using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.FeeServices
{
    public class FeeService : IFeeService
    {
        private MiniSwitchContext _context;

        public FeeService(MiniSwitchContext context)
        {
            _context = context;
        }


        public async Task<ResponseManager> Create(Fee model)
        {
            var fee = new Fee
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                FeeType = model.FeeType,
                Maximum = model.Maximum,
                Minimum = model.Minimum,
            };

            await _context.Fees.AddAsync(fee);
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

        public async Task<ResponseManager> Edit(Fee model)
        {
            var fee = _context.Fees.Where(x => x.Id == model.Id).FirstOrDefault();
            if (fee == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No fee with Id {model.Id} found"
                };
            fee = model;
            _context.Fees.Update(fee);
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

        public async Task<List<Fee>> FetchAll()
        {
            return await _context.Fees.ToListAsync();
        }
    }
}
