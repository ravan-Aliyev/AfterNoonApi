using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.DTOs
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expartion { get; set; }
        public string RefreshToken { get; set; }
    }
}
