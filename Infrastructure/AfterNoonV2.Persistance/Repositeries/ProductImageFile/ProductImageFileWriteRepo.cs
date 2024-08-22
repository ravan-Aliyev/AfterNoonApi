using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance.Repositeries
{
    public class ProductImageFileWriteRepo : WriteRepository<ProductImageFile>, IProductImageFileWriteRepo
    {
        public ProductImageFileWriteRepo(AfterNoonV2DbContext context) : base(context)
        {
        }
    }
}
