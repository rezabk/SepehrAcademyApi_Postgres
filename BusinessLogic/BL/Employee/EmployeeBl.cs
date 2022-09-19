using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Employee;
using DAL.DTO.Course;
using DAL.DTO.Employee;
using DAL.DTO.Lesson;
using DAL.DTO.Student;
using Data;
using Serilog;

namespace BusinessLogic.BL.Employee
{
    public class EmployeeBl : IEmployeeBl
    {
        private readonly IEmployeeRepository _employee;

        private readonly ICourseRepository _course;
        private readonly ITermRepository _term;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly EmployeeValidation _validate;
        private readonly Mapper _mapper;

        public EmployeeBl(IEmployeeRepository employee, ICourseRepository course, ITermRepository term, EmployeeValidation validate, Mapper mapper)
        {
            _employee = employee;

            _course = course;
            _term = term;
            _validate = validate;
            _mapper = mapper;
        }
        public async Task<StandardResult> GetAll()
        {
            var employees = await _employee.GetAllEmployees();

            var checkNullEmployees = await _validate.CheckNullEmployees(employees);
            if (checkNullEmployees.Success == false) return checkNullEmployees;


            var listEmployees = new List<ShowEmployeeDto>();
            foreach (var item in employees)
            {
              
                var tempDto = await _mapper.MapAsync(item, new ShowEmployeeDto());
              
                listEmployees.Add(tempDto);
            }
            var sr = new StandardResult<List<ShowEmployeeDto>>
            {
                Messages = new List<string> { "کارمندان دریافت شدند" },
                Result = listEmployees,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /GetAllEmployees success:true");
            return sr;
        }

        public async Task<StandardResult> GetAllTeachers()
        {
            var employees = await _employee.GetAllTeachers();

            var checkNullEmployees = await _validate.CheckNullEmployees(employees);
            if (checkNullEmployees.Success == false) return checkNullEmployees;

            var listTeachers = new List<ShowEmployeeDto>();
            foreach (var item in employees)
            {
              

            

                var tempDto = await _mapper.MapAsync(item, new ShowEmployeeDto());
             
                listTeachers.Add(tempDto);
            }
            var sr = new StandardResult<List<ShowEmployeeDto>>
            {
                Messages = new List<string> { "معلمان دریافت شدند" },
                Result = listTeachers,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /GetAllTeachers success:true");
            return sr;
        }

        public async Task<StandardResult> GetEmployeeById(int employeeId)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validate.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

           

      
            var showEmployee = await _mapper.MapAsync(employee, new ShowEmployeeDto());
          
            var sr = new StandardResult<ShowEmployeeDto>
            {
                Messages = new List<string> { "کارمند دریافت شد" },
                Result = showEmployee,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /GetEmployeeById success:true");
            return sr;
        }

        public async Task<StandardResult> UpdateEmployee(int employeeId, UpdateEmployeeDto dto)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validate.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            var checkInfo = await _validate.CheckValidateInfo(employeeId, dto);
            if (checkInfo.Success == false) return checkInfo;

            employee.FullName = dto.FullName;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.BirthDate = dto.BirthDate;
            employee.Address = dto.Address;
            employee.Profile = dto.Profile;
            employee.NationalId = dto.NationalId;

            _employee.Save();


            var showUpdatedEmployee = await _mapper.MapAsync(dto, new ShowEmployeeDto());
            showUpdatedEmployee.Email = employee.Email;
            showUpdatedEmployee.Id = employee.Id;
            showUpdatedEmployee.Profile = employee.Profile;
            showUpdatedEmployee.Role = employee.Role;


            var sr = new StandardResult<ShowEmployeeDto>

            {
                Messages = new List<string> { "اطلاعات آپدیت شد" },
                Result = showUpdatedEmployee,
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /UpdateEmployee success:true");
            return sr;
        }


        public async Task<StandardResult> ActiveEmployee(int employeeId)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validate.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            var checkActiveEmployee = await _validate.CheckActiveEmplopyee(employee);
            if (checkActiveEmployee.Success == false) return checkActiveEmployee;

            employee.IsActive = true;

            _employee.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { " کارمند فعال شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /ActiveEmployee success:true");
            return sr;
        }

        public async Task<StandardResult> DeActiveEmployee(int employeeId)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validate.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            var checkDeActiveEmployee = await _validate.CheckDeActiveEmplopyee(employee);
            if (checkDeActiveEmployee.Success == false) return checkDeActiveEmployee;

            employee.IsActive = false;

            _employee.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { "کارمند غیر فعال شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /DeActiveEmployee success:true");
            return sr;
        }

        public async Task<StandardResult> DeleteEmployee(int employeeId)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validate.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            employee.IsDeleted = true;

            _employee.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { " کارمند پاک شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /DelteEmployee success:true");
            return sr;

        }
    }
}
