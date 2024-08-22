using System;
using AfterNoonV2.Application.Enums;

namespace AfterNoonV2.Application.CustomAttributes;

public class AuthorizeDefinitionAttribute : Attribute
{
    public string Menu { get; set; }
    public string Definition { get; set; }

    public ActionsEnum Action { get; set; }
}
