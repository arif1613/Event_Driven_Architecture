using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IReceiptService
    {
        Task AddReceipt(Receipt Receipt);
        Task<List<Receipt>> GetReceipts(Expression<Func<Receipt, bool>> filter, string includeProperties);

    }
}
