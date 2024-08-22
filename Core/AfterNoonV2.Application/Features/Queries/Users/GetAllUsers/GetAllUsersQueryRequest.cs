using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQueryRequest : IRequest<List<GetAllUsersQueryResponse>>
{

}
