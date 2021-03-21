using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Services
{
    public class ReceiptService:IReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReceipt(Receipt Receipt)
        {
            await Task.Run(() => _unitOfWork.ReceiptRepository.Insert(Receipt));
        }

        public async Task<List<Receipt>> GetReceipts(Expression<Func<Receipt, bool>> filter, string includeProperties)
        {
            return await Task.Run(() => _unitOfWork.ReceiptRepository.Get(filter,includeProperties));

        }
    }
}
