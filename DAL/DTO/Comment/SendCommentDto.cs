using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Comment
{
    public class SendCommentDto
    {
        public int PostId { get; set; }
        
        public string Comment { get; set; }
    }
}
