using AfterNoonV2.Application.Repositeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance.Repositeries
{
    public class FileReadRepo : ReadRepository<Domain.Entities.File>, IFileReadRepo
    {
        public FileReadRepo(AfterNoonV2DbContext context) : base(context)
        {
        }
    }
}
