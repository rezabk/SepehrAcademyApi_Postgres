using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Course;
using DAL.DTO.Lesson;

namespace DAL.DTO.Student
{
    public class ShowStudentTermsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Cost { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Capacity { get; set; }

        public int TeacherId { get; set; }

        public int LessonId { get; set; }

      
        public ShowTermTeacherDto TeacherDetails { get; set; }

        public ShowCourseDto CourseDetails { get; set; }
    }
}
