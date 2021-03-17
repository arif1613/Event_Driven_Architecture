using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IOrderLineService
    {
        Task AddOrderLine(OrderLine OrderLine);
        Task AddOrderLines(List<OrderLine> OrderLines);

        Task<OrderLine> GetOrderLineById(int id);
        Task<List<OrderLine>> GetOrderLines(string inculedproperties);

    }
}
