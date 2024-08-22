using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.VerifyResetToken;

public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
