using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Lesson;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Lesson
{
    public class CourseValidation
    {
        private readonly ICourseRepository _course;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public CourseValidation(ICourseRepository course)
        {
            _course = course;
        }
        public async Task<StandardResult> ValidateDto(AddCourseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CourseName))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا نام درس را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /ValidateDto success:false");
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

        public async Task<StandardResult> CheckNullCourses(List<CourseModel> courses)
        {
            if (courses.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ درسی وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullCourses success:false");
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



        public async Task<StandardResult> CheckNullCourse(CourseModel course)
        {
            if (course is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "درسی با ایدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullCourse success:false");
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

        public async Task<StandardResult> CheckActiveCourse(CourseModel course)
        {
            if (course.IsActive == false)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "درس مورد نظر غیرقعال است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CreateCourse success:false");
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


        public async Task<StandardResult> CheckTeacherForUpdateCourse(EmployeeModel employee, CourseModel course)
        {



            if (course.TeacherId != employee.Id)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "شما معلم این دوره نیستید" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /UpdateTerm success:false");
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


    }
}
