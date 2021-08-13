using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Models;

namespace MiniSwitch.Services.TransactionTypeServices
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private MiniSwitchContext _context;

        public TransactionTypeService(MiniSwitchContext context)
        {
            _context = context;
        }


        public async Task<ResponseManager> Create(TransactionType model)
        {
            var transactionType = new TransactionType
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Code = model.Code,
            };

            await _context.TransactionTypes.AddAsync(transactionType);
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

        public async Task<ResponseManager> Edit(TransactionType model)
        {
            var transactionType = _context.TransactionTypes.Where(x => x.Id == model.Id).FirstOrDefault();
            if (transactionType == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = $"No sink node with Id {model.Id} found"
                };
            transactionType = model;
            _context.TransactionTypes.Update(transactionType);
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

        public async Task<List<TransactionType>> FetchAll()
        {
            return await _context.TransactionTypes.ToListAsync();
        }
    }
}
