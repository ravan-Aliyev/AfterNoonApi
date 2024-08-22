using System;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Persistance.Repositeries;

public class MenuReadRepo : ReadRepository<Menu>, IMenuReadRepo
{
    public MenuReadRepo(AfterNoonV2DbContext context) : base(context)
    {
    }
}
