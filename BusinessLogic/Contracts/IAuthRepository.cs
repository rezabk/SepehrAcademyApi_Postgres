using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Auth;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface IAuthRepository
    {
       
        Task<bool> Register(StudentModel student);

        Task<StudentModel> Login(string email,string password);

        Task<bool> CheckEmailForRegister(string email);

        Task<bool> CheckPhoneNumberForRegister(string phoneNumber);

        Task<bool> CheckNationalIdForRegister(string nationalId);



        Task<bool> RegisterEmployee(EmployeeModel employee);

        Task<EmployeeModel> LoginEmployee(string email, string password);

        Task<bool> CheckEmailForRegisterEmployee(string email);

        Task<bool> CheckPhoneNumberForRegisterEmployee(string phoneNumber);

        Task<bool> CheckNationalIdForRegisterEmployee(string nationalId);




        void Save();
    }
}
