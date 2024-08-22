using AfterNoonV2.Application.DTOs.User;
using AfterNoonV2.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string password);

        Task<List<ListUser>> GetAllUsersAsync();
        Task AssignRoleToUserAsync(string userId, string[] roles);

        Task<bool> HasRolePermissionAsync(string userName, string code);
    }
}
