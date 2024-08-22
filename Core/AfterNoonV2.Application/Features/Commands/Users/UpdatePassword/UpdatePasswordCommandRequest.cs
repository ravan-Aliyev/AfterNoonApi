using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
{
    public string UserId { get; set; }
    public string ResetToken { get; set; }
    public string NewPassword { get; set; }
}
