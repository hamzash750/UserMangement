using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UserManagement.Business.Services
{

    public class FileUploadService: IFileUpload
    {

        public async Task<string> Upload([FromForm] IFormFile UImage,string path)
        {
            if (UImage.Length > 0)
            {

                try
                {
                    if (!Directory.Exists(path + "\\Images\\"))
                    {
                        Directory.CreateDirectory(path + "\\Images\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + "" + UImage.FileName))
                    {
                        var fullPath = Path.Combine(path, "Images");
                        var folderName = Path.Combine(fullPath, UImage.FileName);
                        using (var stream = new FileStream(folderName, FileMode.Create))
                        {
                            string DbPath= "/Images/" + UImage.FileName; ;
                               UImage.CopyTo(stream);
                            return DbPath;
                        }
                    }

                }
                catch (Exception ex)
                {

                    return ex.ToString();
                }
            }
            else
            {
                return "Upload Faild";
            }

        }
    }
}
