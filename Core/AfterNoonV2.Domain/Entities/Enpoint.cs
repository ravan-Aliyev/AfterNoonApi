using System;
using AfterNoonV2.Domain.Entities.Common;
using AfterNoonV2.Domain.Entities.Identity;

namespace AfterNoonV2.Domain.Entities;

public class Enpoint : BaseEntity
{
    public Enpoint()
    {
        Roles = new HashSet<AppRole>();
    }
    public string ActionType { get; set; }
    public string HttpType { get; set; }
    public string Definiton { get; set; }
    public string Code { get; set; }
    public Menu Menu { get; set; }
    public ICollection<AppRole> Roles { get; set; }

    public Guid MenuId { get; set; }

}
