using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Employee;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Employee
{
    public class EmployeeValidation
    {
        private readonly IEmployeeRepository _employee;
        private readonly Serilog.ILogger _logger = Log.Logger;
        public EmployeeValidation(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        public async Task<StandardResult> CheckNullEmployees(List<EmployeeModel> employees)
        {
            if (employees.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ کارمندی وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetAllEmployees success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }



        public async Task<StandardResult> CheckNullEmployee(EmployeeModel employee)
        {
            if (employee is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "کارمندی با ایدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetEmployeeById success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }

        public async Task<StandardResult> CheckNullTeacher(EmployeeModel employee)
        {
            if (employee is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "معلمی با ایدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullTeacher success:false");
                return er;
            }

            if (employee.Role != "teacher")
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "کاربر مورد نظر معلم نیست" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullTeacher success:false");
                return er;

            }
            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }



        public async Task<StandardResult> CheckValidateInfo(int employeeId, UpdateEmployeeDto dto)
        {
            var checkPhoneNumber = await _employee.CheckPhoneNumber(dto.PhoneNumber, employeeId);
            if (checkPhoneNumber)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این شماره موبایل قبلا در سایت ثبت نام کرده است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /UpdateEmployee success:false");
                return er;
            }


            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبار سنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }
        public async Task<StandardResult> CheckActiveEmplopyee(EmployeeModel employee)
        {
            if (employee.IsActive)
            {
                var er = new StandardResult

                {
                    Messages = new List<string> { "کارمند فعال است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Information("SepehrAcademyApi : /ActiveEmployee success:false");
                return er;

            }
            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبار سنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }
        public async Task<StandardResult> CheckDeActiveEmplopyee(EmployeeModel employee)
        {
            if (employee.IsActive == false)
            {
                var er = new StandardResult

                {
                    Messages = new List<string> { "کارمند غیر فعال است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Information("SepehrAcademyApi : /DeActiveEmployee success:false");
                return er;

            }
            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبار سنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }


        public async Task<StandardResult> ChechAdmin(EmployeeModel employee)
        {
            if (employee.Role != "admin")
            {
                var er = new StandardResult

                {
                    Messages = new List<string> { "شما ادمین نیستید" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Information("SepehrAcademyApi : /CheckAdmin success:false");
                return er;

            }
            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبار سنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }




    }
}
