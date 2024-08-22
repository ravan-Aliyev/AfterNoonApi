using AfterNoonV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Application.Features.Queries.Product.GetProductById
{
    public class GetProductQueryResponse
    {
        public P.Product Product { get; set; }
    }
}
