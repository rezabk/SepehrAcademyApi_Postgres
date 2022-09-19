using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class LikeModel
    {
        public int Id { get; set; }

        public int TermId { get; set; }

        public int UserId { get; set; }
    }
}
