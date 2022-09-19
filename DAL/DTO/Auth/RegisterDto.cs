using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DAL.DTO.Auth
{
    public class RegisterDto
    {
      
        public string FullName { get; set; }
     
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نیست")]
        public string Email { get; set; }
      
        [MinLength(8,ErrorMessage = "رمز عبور نمی تواند کمتر از 8 کاراکتر باشد")]
        public string Password { get; set; }
    
        public string PhoneNumber { get; set; }

        public string BirthDate { get; set; }

        [AllowNull]
      
        public string? NationalId { get; set; }

        public string? Address { get; set; }

    }
}
