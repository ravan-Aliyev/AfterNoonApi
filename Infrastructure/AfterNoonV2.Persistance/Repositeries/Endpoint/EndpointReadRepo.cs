using System;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Persistance.Repositeries;

public class EndpointReadRepo : ReadRepository<Enpoint>, IEndpointReadRepo
{
    public EndpointReadRepo(AfterNoonV2DbContext context) : base(context)
    {
    }
}
