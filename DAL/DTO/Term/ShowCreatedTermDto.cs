using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Course
{
    public class ShowCreatedTermDto
    {
        public string Title { get; set; }

        public double Cost { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Capacity { get; set; }

        public int Duration { get; set; }

        public int TeacherId { get; set; }

        public int LessonId { get; set; }
    }
}
