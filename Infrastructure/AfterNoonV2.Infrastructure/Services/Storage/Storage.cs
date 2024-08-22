using AfterNoonV2.Application.Abstractions.Storage;
using AfterNoonV2.Infrastructure.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string path, string fileName);

        protected async Task<string> FileRenameAsync(string path, string fileName, HasFile hasFile, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newName = "";
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newName = $"{RenameOperation.CharacterRegulator(oldName)}{extension}";
                }
                else
                {
                    newName = fileName;
                    int indexNo = newName.LastIndexOf("-");
                    if (indexNo == -1)
                        newName = $"{Path.GetFileNameWithoutExtension(newName)}-1{extension}";
                    else
                    {
                        int indexNo1 = newName.IndexOf(".");
                        string num = newName.Substring(indexNo + 1, indexNo1 - indexNo - 1);
                        int fileNo = int.Parse(num);
                        fileNo++;
                        newName = newName.Remove(indexNo + 1, indexNo1 - indexNo - 1).Insert(indexNo + 1, fileNo.ToString());
                    }
                }

                if (hasFile(path, newName))
                {
                    return await FileRenameAsync(path, newName, hasFile, false);
                }
                else
                    return newName;
            });
            return newFileName;
        }
    }
}
