using AfterNoonV2.Application.Repositeries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepo _repo;
        readonly IProductReadRepo _readRepo;

        public UpdateProductCommandHandler(IProductWriteRepo repo, IProductReadRepo readRepo)
        {
            _repo = repo;
            _readRepo = readRepo;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var p = await _readRepo.GetByIdAsync(request.Id);
            p.Name = request.Name;
            p.Stock = request.Stock;
            p.Price = request.Price;
            await _repo.SaveAsync();

            return new();
        }
    }
}
