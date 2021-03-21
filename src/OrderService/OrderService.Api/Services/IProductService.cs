using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<Product> GetProduct(Expression<Func<Product, bool>> filter, string includeProperties);
        Task<List<Product>> GetProducts(Expression<Func<Product, bool>> filter, string includeProperties);

    }
}
