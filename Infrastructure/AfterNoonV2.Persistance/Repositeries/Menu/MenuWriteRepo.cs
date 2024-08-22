using System;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Persistance.Repositeries;

public class MenuWriteRepo : WriteRepository<Menu>, IMenuWriteRepo
{
    public MenuWriteRepo(AfterNoonV2DbContext context) : base(context)
    {
    }
}