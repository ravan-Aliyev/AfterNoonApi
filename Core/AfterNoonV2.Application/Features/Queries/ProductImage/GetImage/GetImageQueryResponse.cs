using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Queries.ProductImage.GetImage
{
    public class GetImageQueryResponse
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
