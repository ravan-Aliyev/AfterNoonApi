using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string path)>> Uploadasync(string path, IFormFileCollection files);
        Task<bool> DeleteAsync(string fileName, string path);

        List<string> GetFiles(string path);
        bool HasFile(string path, string fileName);
    }
}
