using AfterNoonV2.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        readonly private IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            await using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            return Path.Exists(path);
        }

        public async Task<bool> DeleteAsync(string fileName, string path)
        {
            File.Delete($"{path}\\{fileName}");

            return !File.Exists($"{path}\\{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dir = new(path);
            return dir.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string path)>> Uploadasync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> fileInfos = new();

            foreach (IFormFile file in files)
            {
                string newName = await FileRenameAsync(uploadPath, file.FileName, HasFile);

                bool result = await CopyFileAsync($"{uploadPath}\\{newName}", file);

                if (result)
                    fileInfos.Add((file.FileName, $"{path}\\{newName}"));

            }

            return fileInfos;
        }
    }
}
