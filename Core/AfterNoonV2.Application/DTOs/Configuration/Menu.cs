using System;

namespace AfterNoonV2.Application.DTOs.Configuration;

public class Menu
{
    public string Name { get; set; }
    public List<Action> Actions { get; set; } = [];
}
