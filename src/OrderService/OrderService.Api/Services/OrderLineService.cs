using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Api.UnitOfWork;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderLineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrderLine(OrderLine OrderLine)
        {
            await Task.Run(() => _unitOfWork.OrderLineRepository.Insert(OrderLine));
        }

        public async Task AddOrderLines(List<OrderLine> OrderLines)
        {
            await Task.Run(() => _unitOfWork.OrderLineRepository.Insert(OrderLines));

        }

        public async Task<OrderLine> GetOrderLineById(int id)
        {
            return await Task.Run(() => _unitOfWork.OrderLineRepository.Get(id));

        }

        public async Task<List<OrderLine>> GetOrderLines()
        {
            return await Task.Run(() => _unitOfWork.OrderLineRepository.Get().ToList());

        }
    }
}
