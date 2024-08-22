using AfterNoonV2.Application.Abstractions.Hubs;
using AfterNoonV2.Application.Repositeries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepo _repo;
        readonly IProductHubService _productService;

        public CreateProductCommandHandler(IProductWriteRepo repo, IProductHubService productService)
        {
            _repo = repo;
            _productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            bool value = await _repo.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            });

            if (value)
            {
                await _repo.SaveAsync();
            }

            await _productService.ProductAddedMessageAsync("Product added");
            return new()
            {
                IsSuccess = value ? "Succed" : "There is a problem",
            };
        }
    }
}
