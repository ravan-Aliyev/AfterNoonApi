using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Queries.ProductImage.GetImage
{
    public class GetImageQueryRequest : IRequest<IReadOnlyList<GetImageQueryResponse>>
    {
        public string Id { get; set; }
    }
}
