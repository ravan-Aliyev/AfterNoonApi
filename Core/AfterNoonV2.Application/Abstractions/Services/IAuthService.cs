using D = AfterNoonV2.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AfterNoonV2.Application.DTOs;

namespace AfterNoonV2.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<D.Token?> LoginAsync(string userNameOrEmail, string password);
        Task<D.Token?> GoogleLoginAsync(GoogleLoginDTO model);
        Task<D.Token?> RefreshTokenLogin(string refreshToken);
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string userId, string token);
    }
}
