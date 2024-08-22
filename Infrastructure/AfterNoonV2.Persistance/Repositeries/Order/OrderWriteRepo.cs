using AfterNoonV2.Domain.Entities;
using AfterNoonV2.Persistance;
using AfterNoonV2.Persistance.Repositeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Repositeries
{
    public class OrderWriteRepo : WriteRepository<Order>, IOrderWriteRepo
    {
        public OrderWriteRepo(AfterNoonV2DbContext context) : base(context)
        {
        }
    }
}
