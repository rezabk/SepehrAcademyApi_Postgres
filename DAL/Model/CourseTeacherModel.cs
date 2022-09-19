using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class CourseTeacherModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public EmployeeModel Teacher { get; set; }

        public int CourseId { get; set; }

        public CourseModel Course { get; set; }
    }
}
