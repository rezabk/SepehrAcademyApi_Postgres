﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Course
{
    public class ShowTermTeacherDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }

        public string NationalId { get; set; }
        public string Role { get; set; }

        public string? profile { get; set; }
    }
}
