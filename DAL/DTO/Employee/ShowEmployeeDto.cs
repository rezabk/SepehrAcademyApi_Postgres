using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Student;

namespace DAL.DTO.Employee
{
    public class ShowEmployeeDto
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
