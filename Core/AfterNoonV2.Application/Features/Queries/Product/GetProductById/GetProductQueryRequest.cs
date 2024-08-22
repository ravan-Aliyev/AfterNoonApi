using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Queries.Product.GetProductById
{
    public class GetProductQueryRequest : IRequest<GetProductQueryResponse>
    {
        public string ProductId { get; set; }
    }
}
