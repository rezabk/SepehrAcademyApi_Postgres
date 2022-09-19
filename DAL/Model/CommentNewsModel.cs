using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class CommentNewsModel
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Email { get; set; }

    
        public string Comment { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsVerified { get; set; }

        public CommentNewsModel()
        {
            IsDeleted = false;
            IsVerified = false;
        }
    }
}
