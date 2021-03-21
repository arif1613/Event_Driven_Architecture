using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Event;
using OrderService.Api.Services;

namespace OrderService.Api.RequestHandlers
{
    public class ProductCreatedHandler: IRequestHandler<ProductCreated,Unit>
    {
        private readonly IProductEventService _productEventService;
        private readonly IProductService _productService;

        public ProductCreatedHandler(IProductService productService, IProductEventService productEventService)
        {
            _productService = productService;
            _productEventService = productEventService;
        }

        public async Task<Unit> Handle(ProductCreated request, CancellationToken cancellationToken)
        {
            var product = await _productEventService.GetProduct(r => r.MessageId == request.MessageId, null);

            if (product == null)
            {
                Console.WriteLine("No product found");
            }
            else
            {
                await _productService.AddProduct(product);
            }

            return Unit.Value;
        }
    }
}
