using AfterNoonV2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public Guid CustomerId { get; set; }

        public ICollection<Product> Products { get; set; }
        public Basket Basket { get; set; }
        public Customer Customer { get; set; }
    }
}
