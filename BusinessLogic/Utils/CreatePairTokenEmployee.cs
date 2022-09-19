using DAL.Model;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public class CreatePairTokenEmployee
    {
        private readonly GenerateToken _jwtHandler;

        public CreatePairTokenEmployee(GenerateToken jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }
        public (PairTokenDto, double) GeneratePairToken(EmployeeModel employee)
        {
            var claims = new List<Claim>
           {
               new(ClaimTypes.Name, employee.Id.ToString()),
               new(ClaimTypes.Role, employee.Role.ToString())
           };

            return (new PairTokenDto
            {
                AccessToken = _jwtHandler.CreateAccessToken(claims),
                RefreshToken = _jwtHandler.CreateRefreshToken()
            }
                , _jwtHandler.GetRefreshTokenExTime());
        }
    }
}
