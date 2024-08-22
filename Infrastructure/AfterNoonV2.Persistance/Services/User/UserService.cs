using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.DTOs.User;
using AfterNoonV2.Application.Helpers;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance.Services.User
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEndpointReadRepo _endpointReadRepo;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepo endpointReadRepo)
        {
            _userManager = userManager;
            _endpointReadRepo = endpointReadRepo;
        }

        public async Task<List<ListUser>> GetAllUsersAsync()
        {
            var listUser = _userManager.Users.Select(x => new ListUser
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName
            }).ToList();

            return listUser;
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            CreateUserResponseDTO response = new() { Success = identityResult.Succeeded };
            if (identityResult.Succeeded)
                response.Message = "User created successfully";
            else
                foreach (var error in identityResult.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string password)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            resetToken = resetToken.UrlDecode();

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, resetToken, password);

            if (identityResult.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new Exception("Can not change password");
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.EndDate = accessTokenDate.AddMinutes(15);
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<bool> HasRolePermissionAsync(string userName, string code)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return false;

            var userRoles = await _userManager.GetRolesAsync(user);
            var listRoles = userRoles.ToArray();

            if (listRoles.Length == 0)
                return false;

            var endpoints = _endpointReadRepo.Table.Include(e => e.Roles).FirstOrDefault(e => e.Code == code);

            if (endpoints == null)
                return false;

            var endpointsRoles = endpoints.Roles.Select(r => r.Name);

            foreach (var role in listRoles)
                if (endpointsRoles.Contains(role))
                    return true;
            
            return false;
        }
    }
}
