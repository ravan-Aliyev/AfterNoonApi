using AfterNoonV2.Application.Repositeries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Queries.Product.GetProductById
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, GetProductQueryResponse>
    {
        readonly IProductReadRepo _repo;

        public GetProductQueryHandler(IProductReadRepo repo)
        {
            _repo = repo;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            return new()
            {
                Product = await _repo.GetByIdAsync(request.ProductId),
            };
        }
    }
}
