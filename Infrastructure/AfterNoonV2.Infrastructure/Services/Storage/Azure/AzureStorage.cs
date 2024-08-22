using AfterNoonV2.Application.Abstractions.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _containerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }


        public async Task<bool> DeleteAsync(string fileName, string container)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient client = _containerClient.GetBlobClient(fileName);
            await client.DeleteAsync();
            bool exists = await client.ExistsAsync();
            return !exists;
        }

        public List<string> GetFiles(string container)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            return _containerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public new bool HasFile(string container, string fileName)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient client = _containerClient.GetBlobClient(fileName);
            return client.Exists();
        }

        public async Task<List<(string fileName, string path)>> Uploadasync(string container, IFormFileCollection files)
        {
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);

            await _containerClient.CreateIfNotExistsAsync();
            await _containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string path)> datas = [];

            foreach (var file in files)
            {
                string newFileName = await FileRenameAsync(container, file.FileName, this.HasFile);

                BlobClient client = _containerClient.GetBlobClient(newFileName);
                await client.UploadAsync(file.OpenReadStream());
                datas.Add((newFileName, $"{container}/{newFileName}"));
            }

            return datas;
        }
    }
}
