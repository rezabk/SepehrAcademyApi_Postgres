using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace DAL.DTO.Upload
{
    public class UploadImageDto
    {
        public IFormFile Image { get; set; }
    }
}
