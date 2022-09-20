using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DAL.DTO.Upload
{
    public class UploadDto
    {
        public IFormFile Image { get; set; }
    }
}
