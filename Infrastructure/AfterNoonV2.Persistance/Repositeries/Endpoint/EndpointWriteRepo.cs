using System;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Persistance.Repositeries;

public class EndpointWriteRepo : WriteRepository<Enpoint>, IEndpointWriteRepo
{
    public EndpointWriteRepo(AfterNoonV2DbContext context) : base(context)
    {
    }
}
