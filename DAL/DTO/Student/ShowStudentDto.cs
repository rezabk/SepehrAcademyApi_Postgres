using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Course;

namespace DAL.DTO.Student
{
    public class ShowStudentDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }

        public string NationalId { get; set; }
        public string Role { get; set; }

        public string? Profile { get; set; }

        public string? Address { get; set; }

        public List<ShowStudentTermsDto> TermDetails { get; set; }

    }
}
