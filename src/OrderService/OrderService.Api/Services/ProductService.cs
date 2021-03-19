using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Services
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProduct(Product product)
        {
            await Task.Run(() => _unitOfWork.ProductRepository.Insert(product));
        }

        public async Task AddProducts(List<Product> products)
        {
            await Task.Run(() => _unitOfWork.ProductRepository.Insert(products));

        }

        public async Task<Product> GetProduct(Expression<Func<Product, bool>> filter, string includeProperties)
        {
            var products= await Task.Run(() => _unitOfWork.ProductRepository.Get(filter, includeProperties));
            if (!products.Any())
            {
                return null;
            }
            return products.FirstOrDefault();
        }

        public async Task<List<Product>> GetProducts(Expression<Func<Product, bool>> filter, string includeProperties)
        {
            return await Task.Run(() => _unitOfWork.ProductRepository.Get(filter,includeProperties));

        }
    }
}
