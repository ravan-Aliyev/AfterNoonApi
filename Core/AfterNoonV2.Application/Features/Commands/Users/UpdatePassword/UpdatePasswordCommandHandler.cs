using AfterNoonV2.Application.Abstractions.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.NewPassword);
        return new();
    }
}
