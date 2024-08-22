using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Exceptions
{
    public class CreateUserFailExcepton : Exception
    {
        public CreateUserFailExcepton(string? message) : base(message)
        {
        }

        public CreateUserFailExcepton(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
