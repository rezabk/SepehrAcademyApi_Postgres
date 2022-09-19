using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ContactUsModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsVerified { get; set; }
    }
}
