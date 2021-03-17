using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Api.UnitOfWork;
using OrderService.Data.Models;

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

        public async Task<Product> GetProductById(int id)
        {
            return await Task.Run(() => _unitOfWork.ProductRepository.Get(id));

        }

        public async Task<List<Product>> GetProducts()
        {
            return await Task.Run(() => _unitOfWork.ProductRepository.Get().ToList());

        }
    }
}
