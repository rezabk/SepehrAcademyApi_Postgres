using DAL;
using DAL.Model;
using Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Student;

namespace BusinessLogic.Utils.Student
{
    public class StudentValidation
    {

        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly IStudentRepository _student;

        public StudentValidation(IStudentRepository student)
        {
            _student = student;
        }

        public async Task<StandardResult> CheckNullStudents(List<StudentModel> students)
        {
            if (students.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ دانش آموزی وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetAllStudents success:false");
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

        public async Task<StandardResult> CheckNullStudent(StudentModel student)
        {
            if (student is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "دانش آموزی با آیدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetStudentById success:false");
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

        public async Task<StandardResult> CheckValidateInfo(int studentId, UpdateStudentDto dto)
        {
            var checkPhoneNumber = await _student.CheckPhoneNumber(dto.PhoneNumber, studentId);
            if (checkPhoneNumber)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این شماره موبایل قبلا در سایت ثبت نام کرده است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /UpdateStudent success:false");
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

        public async Task<StandardResult> CheckActiveStudent(StudentModel student)
        {
            if (student.IsActive)
            {
                var er = new StandardResult

                {
                    Messages = new List<string> { "دانش آموز فعال است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Information("SepehrAcademyApi : /ActiveStudent success:false");
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
        public async Task<StandardResult> CheckDeActiveStudent(StudentModel student)
        {
            if (student.IsActive == false)
            {
                var er = new StandardResult

                {
                    Messages = new List<string> { "دانش آموز غیر فعال است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Information("SepehrAcademyApi : /DeActiveStudent success:false");
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