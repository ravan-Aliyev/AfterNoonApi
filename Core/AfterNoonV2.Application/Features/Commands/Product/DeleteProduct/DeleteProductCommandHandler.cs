using AfterNoonV2.Application.Repositeries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductWriteRepo _repo;

        public DeleteProductCommandHandler(IProductWriteRepo repo)
        {
            _repo = repo;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _repo.RemoveAsync(request.ProductId);
            await _repo.SaveAsync();
            return new();
        }
    }
}
