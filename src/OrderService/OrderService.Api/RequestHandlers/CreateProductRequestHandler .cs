using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Request;
using OrderService.Api.Services;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProduct, ProductCreatedEvent>
    {
        private readonly IProductEventService _productEventService;
        public CreateProductRequestHandler(IProductEventService productEventService)
        {
            _productEventService = productEventService;
        }

        public async Task<ProductCreatedEvent> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            var messageid = Guid.NewGuid();
            var productCreatedEvent = new ProductCreatedEvent
            {
                MessageId = messageid,
                Id = Guid.NewGuid(),
                ProductName = request.ProductName,
                ProductType = request.ProductType,
                Price = request.Price,
                HasDisabilityDiscount = request.HasDisabilityDiscount,
                HasFlatDiscount = request.HasFlatDiscount,
                HasQuantityDiscount = request.HasQuantityDiscount
            };
            await _productEventService.AddEvent(productCreatedEvent);
            return productCreatedEvent;
        }
    }
}
