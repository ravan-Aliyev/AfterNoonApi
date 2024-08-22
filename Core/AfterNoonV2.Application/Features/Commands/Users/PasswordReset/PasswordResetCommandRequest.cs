using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.PasswordReset;

public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
{
    public string Email { get; set; }
}
