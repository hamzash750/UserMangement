﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Business.Interfaces
{
    public interface IFileUpload
    {
        Task<string> Upload([FromForm] IFormFile UImage, string path);
    }
}

