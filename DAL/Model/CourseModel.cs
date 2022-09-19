using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class CourseModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public EmployeeModel Teacher { get; set; }

        [Required]
        [MaxLength(255)]
        public string[] Topics { get; set; }

        [AllowNull]
        [MaxLength(255)]
        public string? Description { get; set; }

        [AllowNull]
        public string? Image { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        public List<TermModel> Terms { get; set; }

      

        public CourseModel()
        {
            Terms = new List<TermModel>();
            IsDeleted = false;
            IsActive = true;
        }

    }
}
