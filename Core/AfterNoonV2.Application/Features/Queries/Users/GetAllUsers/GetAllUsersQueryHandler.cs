using System;
using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AfterNoonV2.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<GetAllUsersQueryResponse>>
{
    readonly IUserService _userService;
    public GetAllUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var usersList = await _userService.GetAllUsersAsync();

        return usersList.Select(x => new GetAllUsersQueryResponse
        {
            Id = x.Id,
            UserName = x.UserName,
            Email = x.Email
        }).ToList();
    }
}
