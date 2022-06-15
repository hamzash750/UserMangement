using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.ViewModel
{
    public class FileUploadVM
    {
        public int UId { get; set; }
        public IFormFile UserImage { get; set; }
    }
}
