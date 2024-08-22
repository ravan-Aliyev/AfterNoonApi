//using AfterNoonV2.Application.Services;
//using AfterNoonV2.Infrastructure.StaticServices;
//using Azure.Core;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AfterNoonV2.Infrastructure.Services
//{
//    public class FileService : IFileService
//    {
//        readonly private IWebHostEnvironment _webHostEnvironment;

//        public FileService(IWebHostEnvironment webHostEnvironment)
//        {
//            _webHostEnvironment = webHostEnvironment;
//        }

//        public async Task<bool> CopyFileAsync(string path, IFormFile file)
//        {
//            await using (FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false))
//            {
//                await file.CopyToAsync(stream);
//                await stream.FlushAsync();
//            }

//            return Path.Exists(path);
//        }

//        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
//        {
//            string newFileName = await Task.Run<string>(async () =>
//            {
//                string extension = Path.GetExtension(fileName);
//                string newName = "";
//                if (first)
//                {
//                    string oldName = Path.GetFileNameWithoutExtension(fileName);
//                    newName = $"{RenameOperation.CharacterRegulator(oldName)}{extension}";
//                }
//                else
//                {
//                    newName = fileName;
//                    int indexNo = newName.LastIndexOf("-");
//                    if (indexNo == -1)
//                        newName = $"{Path.GetFileNameWithoutExtension(newName)}-1{extension}";
//                    else
//                    {
//                        int indexNo1 = newName.IndexOf(".");
//                        string num = newName.Substring(indexNo + 1, indexNo1 - indexNo - 1);
//                        int fileNo = int.Parse(num);
//                        fileNo++;
//                        newName = newName.Remove(indexNo + 1, indexNo1 - indexNo - 1).Insert(indexNo + 1, fileNo.ToString());
//                    }
//                }

//                if (File.Exists($"{path}\\{newName}"))
//                {
//                    return await FileRenameAsync(path, newName, false);
//                }
//                else 
//                    return newName;
//            });

//            return newFileName;
//        }

//        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
//        {
//            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

//            if (!Directory.Exists(uploadPath))
//                Directory.CreateDirectory(uploadPath);

//            List <(string fileName, string path)> fileInfos = new();

//            foreach (IFormFile file in files)
//            {
//                string newName = await FileRenameAsync(uploadPath, file.FileName);

//                bool result = await CopyFileAsync($"{uploadPath}\\{newName}", file);

//                if (result)
//                    fileInfos.Add((newName, $"{path}\\{newName}"));

//            }

//            return fileInfos;
//        }
//    }
//}
