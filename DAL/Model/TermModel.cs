using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TermModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Cost { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Capacity { get; set; }

        public int Duration { get; set; }

        public int TeacherId { get; set; }

        public EmployeeModel Teacher { get; set; }

        public int CourseId { get; set; }

        public CourseModel Course { get; set; }

        public List<StudentModel> Students { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public List<TermStudentModel> TermStudent { get; set; }

  
        public TermModel()
        {
            Students = new List<StudentModel>();
            TermStudent = new List<TermStudentModel>();
            IsDeleted = false;
            IsActive = true;
        }
    }
}
