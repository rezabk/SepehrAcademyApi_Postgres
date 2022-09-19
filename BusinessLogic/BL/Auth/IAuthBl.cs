using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Auth;
using Data;

namespace BusinessLogic.BL.Auth
{
    public interface IAuthBl
    {
        Task<StandardResult> Register(RegisterDto dto);

        Task<StandardResult> Login(LoginDto dto);

        Task<StandardResult> RegisterEmployee(RegisterDto dto);

        Task<StandardResult> LoginEmployee(LoginDto dto);
    }
}
