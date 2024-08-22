using System;
using AfterNoonV2.Application.DTOs.Configuration;

namespace AfterNoonV2.Application.Abstractions.Services;

public interface IAppService
{
    List<Menu> GetAuthorizeDefinitionEndPoint(Type type);
}
