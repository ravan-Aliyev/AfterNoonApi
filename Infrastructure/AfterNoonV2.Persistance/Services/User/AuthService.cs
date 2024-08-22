using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.Abstractions.Token;
using AfterNoonV2.Application.DTOs;
using AfterNoonV2.Application.Helpers;
using AfterNoonV2.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance.Services.User
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _manager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;
        readonly IUserService _userService;

        readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> manager, ITokenHandler handler, SignInManager<AppUser> signInManager, IConfiguration configuration, IUserService userService, IMailService mailService)
        {
            _manager = manager;
            _tokenHandler = handler;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<Token?> GoogleLoginAsync(GoogleLoginDTO model)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Auth:Google"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);

            UserLoginInfo info = new(model.Provider, payload.Subject, model.Provider);

            AppUser? user = await _manager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    NameSurname = payload.Name,
                    UserName = payload.Name,
                };

                IdentityResult identityResult = await _manager.CreateAsync(user);
                result = identityResult.Succeeded;
            }

            if (result)
                await _manager.AddLoginAsync(user, info);
            else
                throw new Exception("Can not auth");

            Token token = _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expartion);


            return token;
        }

        public async Task<Token?> LoginAsync(string userNameOrEmail, string password)
        {
            AppUser? user = await _manager.FindByNameAsync(userNameOrEmail);

            if (user == null)
                user = await _manager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
                throw new Exception("There is no such user");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expartion);

                return token;
            }

            return null;
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser? user = await _manager.FindByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            string resetToken = await _manager.GeneratePasswordResetTokenAsync(user);
            resetToken = resetToken.UrlEncode();
            await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
        }

        public async Task<Token> RefreshTokenLogin(string refreshToken)
        {
            AppUser? user = await _manager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user != null && user?.EndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(user);

                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expartion);

                return token;
            }
            else
                throw new Exception("User not found");
        }

        public async Task<bool> VerifyResetTokenAsync(string userId, string token)
        {
            AppUser? user = await _manager.FindByIdAsync(userId);

            if (user == null)
                return false;

            token = token.UrlDecode();

            return await _manager.VerifyUserTokenAsync(user, _manager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        }
    }
}
