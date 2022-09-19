using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployees();

        Task<List<EmployeeModel>> GetAllTeachers();

        Task<EmployeeModel> GetEmployeeById(int employeeId);

        Task<bool> CheckPhoneNumber(string phoneNumber,int employeeId);

        Task<bool> CheckNationalId(string nationalId, int employeeId);

        public void Save();
    }
}
