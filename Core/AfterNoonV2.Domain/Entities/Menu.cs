using System;
using AfterNoonV2.Domain.Entities.Common;

namespace AfterNoonV2.Domain.Entities;

public class Menu : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Enpoint> Enpoints { get; set; }
}
