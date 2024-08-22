using AfterNoonV2.Application.Repositeries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Application.Features.Queries.ProductImage.GetImage
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQueryRequest, IReadOnlyList<GetImageQueryResponse>>
    {
        readonly IProductReadRepo _repo;

        public GetImageQueryHandler(IProductReadRepo repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<GetImageQueryResponse>?> Handle(GetImageQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? pro = await _repo.Table.Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return pro?.ProductImages.Select(i => new GetImageQueryResponse
            {
                Id = i.Id,
                Name = i.FileName,
                Path = i.Path,
            }).ToList();
        }
    }
}
