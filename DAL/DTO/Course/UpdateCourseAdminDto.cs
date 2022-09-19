﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Course
{
    public class UpdateCourseAdminDto
    {
        public string CourseName { get; set; }

        public string[] Topics { get; set; }

        public string Description { get; set; }


        public string Image { get; set; }

        public int TeacherId { get; set; }
    }
}
