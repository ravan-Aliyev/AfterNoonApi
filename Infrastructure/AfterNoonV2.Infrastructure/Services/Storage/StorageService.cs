using AfterNoonV2.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Infrastructure.Services
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string Storage { get => _storage.GetType().Name; }

        public async Task<bool> DeleteAsync(string fileName, string path)
        {
            return await _storage.DeleteAsync(fileName, path);
        }

        public List<string> GetFiles(string path)
        {
            return _storage.GetFiles(path);
        }

        public bool HasFile(string path, string fileName)
        {
            return _storage.HasFile(path, fileName);
        }

        public async Task<List<(string fileName, string path)>> Uploadasync(string path, IFormFileCollection files)
        {
            return await _storage.Uploadasync(path, files);
        }
    }
}
