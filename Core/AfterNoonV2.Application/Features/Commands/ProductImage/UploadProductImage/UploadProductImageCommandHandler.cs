using AfterNoonV2.Application.Abstractions.Storage;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;
using Azure.Core;
using P = AfterNoonV2.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Commands.ProductImage.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepo _repo;
        readonly IProductImageFileWriteRepo _fileWriteRepo;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepo repo, IProductImageFileWriteRepo fileWriteRepo)
        {
            _storageService = storageService;
            _repo = repo;
            _fileWriteRepo = fileWriteRepo;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _storageService.Uploadasync("product-images", request.Files);

            P.Product finded = await _repo.GetByIdAsync(request.Id);

            await _fileWriteRepo.AddRangeAsync(result.Select(d => new ProductImageFile
            {
                FileName = d.fileName,
                Path = d.path,
                Storage = _storageService.Storage,
                Products = [finded]
            }).ToList());

            await _fileWriteRepo.SaveAsync();

            return new();
        }
    }
}
