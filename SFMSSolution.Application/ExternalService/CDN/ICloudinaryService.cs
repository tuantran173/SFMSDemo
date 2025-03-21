﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.ExternalService.CDN
{
    public interface ICloudinaryService
    {
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
