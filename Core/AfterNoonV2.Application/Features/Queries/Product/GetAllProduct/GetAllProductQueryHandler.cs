using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AfterNoonV2.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepo _repoRead;

        public GetAllProductQueryHandler(IProductReadRepo repo)
        {
            _repoRead = repo;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _repoRead.GetAll().Count();
            throw new Exception("Senin anani");
            var products = _repoRead.GetAll(false)
                .Skip(request.Size * request.Page)
                .Take(request.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.CreateDate,
                    p.Price,
                });

            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
