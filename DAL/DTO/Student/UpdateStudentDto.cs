using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Student
{
    public class UpdateStudentDto
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }

        [AllowNull]
        public string? Address { get; set; }

        public string? NationalId { get; set; }

        public string? Profile { get; set; }
    }
}
