using AfterNoonV2.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Queries.Product.GetAllProduct
{
    public record GetAllProductQueryRequest : Pagination, IRequest<GetAllProductQueryResponse>
    {
    }
}
