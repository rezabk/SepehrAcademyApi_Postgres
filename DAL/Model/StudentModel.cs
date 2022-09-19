using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }
        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? BirthDate { get; set; }
        [MaxLength(10)]
        [MinLength(10)]
        public string? NationalId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Role { get; set; }
        [MaxLength(500)]
        public string? Profile { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        [MaxLength(500)]
        public string? RefreshTokenHash { get; set; }
        [MaxLength(500)]
        public DateTime? RefreshTokenExTime { get; set; }

        public List<TermStudentModel> TermStudent { get; set; }

        public StudentModel()
        {
            IsActive = true;
            IsDeleted = false;
            Role = "student";
        }
        public void Refresh(string refreshTokenHash, DateTime exp)
        {
            RefreshTokenHash = refreshTokenHash;
            RefreshTokenExTime = exp;
        }
        public bool ValidateRefreshToken(string token)
        {
            return RefreshTokenHash.Equals(token);
        }

    }
}
