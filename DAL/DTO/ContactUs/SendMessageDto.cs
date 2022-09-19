using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.ContactUs
{
    public class SendMessageDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}
