using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSwitch.Data;
using MiniSwitch.Helpers;
using MiniSwitch.Models;
using MiniSwitch.ViewModels;

namespace MiniSwitch.Services.TransactionsServices
{
    public class TransactionService : ITransactionService
    {
        private MiniSwitchContext _context;

        public TransactionService(MiniSwitchContext context)
        {
            _context = context;
        }

        public async Task<ResponseManager> Create(TransactionViewModel model)
        {
            var sourceNode = await _context.SourceNodes.Where(x => x.Id == model.SourceNodeID).Include(s => s.Scheme).ThenInclude(f => f.Fee).FirstOrDefaultAsync();

            if (sourceNode == null)
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Select a valid scheme"
                };
            var fee = FeeGenerationHelper.CalculateFee(model.Amount, sourceNode.Scheme.Fee);
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = model.Amount,
                TotalAmount = model.Amount + fee,
                Fee = fee,
                SourceNode = sourceNode,
            };

            await _context.Transactions.AddAsync(transaction);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new ResponseManager
                {
                    isSuccess = true,
                    Message = "Transaction Successful"
                };
            }
            else
            {
                return new ResponseManager
                {
                    isSuccess = false,
                    Message = "Transaction Failed"
                };
            }
        }

        public async Task<List<Transaction>> FetchAll(string searchString = "", int? pageNumber = 1)
        {
            int pageSize = 15;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                var result = _context.Transactions
                    .Include(s => s.SourceNode)
                        .ThenInclude(s => s.Scheme)
                            .ThenInclude(r => r.Route)
                                .ThenInclude(s => s.SinkNode)
                    .Include(c => c.SourceNode.Scheme.Channel)
                    .Include(t => t.SourceNode.Scheme.TransactionType)
                    .Where(s => s.SourceNode.Scheme.Name.Contains(searchString) || s.SourceNode.Name.Contains(searchString) || s.Amount.ToString().Contains(searchString) || s.SourceNode.Scheme.Route.Name.Contains(searchString) || s.SourceNode.Scheme.Channel.Name.Contains(searchString) || s.SourceNode.Scheme.Fee.Name.Contains(searchString))
                    .AsNoTracking();
                return await PaginatedList<Transaction>.CreateAsync(result, pageNumber ?? 1, pageSize);
            }
            return await PaginatedList<Transaction>.CreateAsync(
                _context.Transactions
                    .Include(s => s.SourceNode)
                        .ThenInclude(s => s.Scheme)
                            .ThenInclude(r => r.Route)
                                .ThenInclude(s => s.SinkNode)
                    .Include(c => c.SourceNode.Scheme.Channel)
                    .Include(t => t.SourceNode.Scheme.TransactionType)
                    .AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
