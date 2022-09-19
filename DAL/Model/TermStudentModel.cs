using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TermStudentModel
    {
        public int Id { get; set; }

        public int TermId { get; set; }
        public TermModel Term { get; set; }

        public int StudentId { get; set; }

        public StudentModel Student { get; set; }


    }
}
