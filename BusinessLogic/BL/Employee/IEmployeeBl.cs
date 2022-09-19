using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Employee;
using Data;

namespace BusinessLogic.BL.Employee
{
    public interface IEmployeeBl
    {
        Task<StandardResult> GetAll();

        Task<StandardResult> GetAllTeachers();

        Task<StandardResult> GetEmployeeById(int employeeId);

        Task<StandardResult> UpdateEmployee(int employeeId, UpdateEmployeeDto dto);

        Task<StandardResult> ActiveEmployee(int employeeId);

        Task<StandardResult> DeActiveEmployee(int employeeId);

        Task<StandardResult> DeleteEmployee(int employeeId);
    }
}
