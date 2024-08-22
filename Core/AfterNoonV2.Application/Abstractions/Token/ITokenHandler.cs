using AfterNoonV2.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = AfterNoonV2.Application.DTOs;

namespace AfterNoonV2.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        D.Token CreateAccessToken(AppUser user);
        string CreateRefreshToken();

    }
}
