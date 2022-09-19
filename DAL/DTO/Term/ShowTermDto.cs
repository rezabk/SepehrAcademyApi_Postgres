using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Employee;
using DAL.DTO.Lesson;
using DAL.DTO.Student;
using DAL.Model;

namespace DAL.DTO.Course
{
    public class ShowTermDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Cost { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Capacity { get; set; }

        public int Duration { get; set; }

        public int TeacherId { get; set; }

        public int LessonId { get; set; }

        public List<ShowStudentDto> StudentDetails { get; set; }

        public ShowTermTeacherDto TeacherDetails { get; set; }

        public ShowCourseDto CourseDetails { get; set; }
    }
}
