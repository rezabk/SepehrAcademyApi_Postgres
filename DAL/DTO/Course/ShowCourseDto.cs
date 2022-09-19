using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Lesson
{
    public class ShowCourseDto
    {

        public int Id { get; set; }

        public string CourseName { get; set; }

        public int TeacherId { get; set; }


        public List<string> Topics { get; set; }


        public string Description { get; set; }


        public string Image { get; set; }
    }
}
