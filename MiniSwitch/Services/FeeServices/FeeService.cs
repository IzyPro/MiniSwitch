using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Enums;
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
            if(model.FeeType == FeeTypeEnum.Flat && model.Amount <= 0m)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Flat Fees must have a valid amount"
                };
            }
            else if(model.FeeType == FeeTypeEnum.Percentage && (model.Maximum <= 0m || model.Minimum <= 0m || model.Percentage < 0))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Percentage Fees must have valid percentage, maximum & minimum value"
                };
            }

            var fee = new Fee
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                FeeType = model.FeeType,
                Maximum = model.Maximum,
                Minimum = model.Minimum,
                Amount = model.Amount,
                Percentage = model.Percentage,
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
            if (model.FeeType == FeeTypeEnum.Flat && model.Amount <= 0m)
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Flat Fees must have a valid amount"
                };
            }
            else if (model.FeeType == FeeTypeEnum.Percentage && (model.Maximum <= 0m || model.Minimum <= 0m || model.Percentage < 0))
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Percentage Fees must have valid percentage, maximum & minimum value"
                };
            }

            var fee = _context.Fees.Where(x => x.Id == model.Id).FirstOrDefault();
            if (fee == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No fee with Id {model.Id} found"
                };

            fee.Name = model.Name;
            fee.FeeType = model.FeeType;
            fee.Maximum = model.Maximum;
            fee.Minimum = model.Minimum;
            fee.Amount = model.Amount;
            fee.Percentage = model.Percentage;

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

        public async Task<List<Fee>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.Fees
                    .Where(s => s.Name.Contains(searchString) || s.Maximum.ToString().Contains(searchString) || s.Minimum.ToString().Contains(searchString) || s.Percentage.ToString().Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<Fee>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<Fee>.CreateAsync(_context.Fees.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
