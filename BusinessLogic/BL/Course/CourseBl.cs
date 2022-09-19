using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Employee;
using BusinessLogic.Utils.Lesson;
using DAL.DTO.Course;
using DAL.DTO.Lesson;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.Lesson
{
    public class CourseBl : ICourseBl
    {
        private readonly ICourseRepository _course;
        private readonly IEmployeeRepository _employee;
        private readonly ICourseTeacherRepository _courseTeacher;
        private readonly EmployeeValidation _validateEmployee;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly CourseValidation _validate;
        private readonly Mapper _mapper;
        private readonly ShamasiCalendar _shamsi;

        public CourseBl(ICourseRepository course, IEmployeeRepository employee, ICourseTeacherRepository courseTeacher, EmployeeValidation validateEmployee, CourseValidation validate, Mapper mapper, ShamasiCalendar shamsi)
        {
            _course = course;
            _employee = employee;
            _courseTeacher = courseTeacher;
            _validateEmployee = validateEmployee;
            _validate = validate;
            _mapper = mapper;
            _shamsi = shamsi;
        }
        public async Task<StandardResult> AddCourse(int userId,AddCourseDto dto)
        {
            var teacher = await _employee.GetEmployeeById(userId);
            var checkNullTeacher = await _validateEmployee.CheckNullTeacher(teacher);
            if (checkNullTeacher.Success == false) return checkNullTeacher;

            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.Success == false) return validateDto;

            var newCourse = new CourseModel
            {
                TeacherId = teacher.Id,
                Teacher = teacher,
                CourseName = dto.CourseName,
                Description = dto.Description,
                Image = dto.Image,
                Topics = dto.Topics,
                IsDeleted = false,
                IsActive = true,
                CreationDate = _shamsi.ToShamsi(DateTime.UtcNow),
            };
            await _course.AddCourse(newCourse);

            var newCourseTeacher = new CourseTeacherModel
            {
                Course = newCourse,
                CourseId = newCourse.Id,
                Teacher = teacher,
                TeacherId = teacher.Id
            };
            await _courseTeacher.Add(newCourseTeacher);

            

            var showCourse = await _mapper.MapAsync(newCourse, new ShowCourseDto());

            var sr = new StandardResult<ShowCourseDto>
            {
                Messages = new List<string> { "درس با موفقیت ساخته شد" },
                Success = true,
                Result = showCourse,
                StatusCode = 201,
            };
            _logger.Error("SepehrAcademyApi : /AddCourse success:true");
            return sr;
        }

        public async Task<StandardResult> AddCourseAdmin(int userId, AddCourseAdminDto dto)
        {
            var admin = await _employee.GetEmployeeById(userId);
            var checkNullAdmin = await _validateEmployee.CheckNullEmployee(admin);
            if (checkNullAdmin.Success == false) return checkNullAdmin;

            var checkAdmin = await _validateEmployee.ChechAdmin(admin);
            if (checkAdmin.Success == false) return checkAdmin;


            var teacher = await _employee.GetEmployeeById(dto.TeacherId);
            var checkNullTeacher = await _validateEmployee.CheckNullTeacher(teacher);
            if (checkNullTeacher.Success == false) return checkNullTeacher;

            var newCourse = new CourseModel
            {
                TeacherId = teacher.Id,
                Teacher = teacher,
                CourseName = dto.CourseName,
                Description = dto.Description,
                Image = dto.Image,
                Topics = dto.Topics,
                IsDeleted = false,
                IsActive = true,
                CreationDate = _shamsi.ToShamsi(DateTime.UtcNow),
            };
            await _course.AddCourse(newCourse);

            var newCourseTeacher = new CourseTeacherModel
            {
                Course = newCourse,
                CourseId = newCourse.Id,
                Teacher = teacher,
                TeacherId = teacher.Id
            };
            await _courseTeacher.Add(newCourseTeacher);

            var showCourse = await _mapper.MapAsync(newCourse, new ShowCourseDto());

            var sr = new StandardResult<ShowCourseDto>
            {
                Messages = new List<string> { "درس با موفقیت ساخته شد" },
                Success = true,
                Result = showCourse,
                StatusCode = 201,
            };
            _logger.Error("SepehrAcademyApi : /AddCourse success:true");
            return sr;

        }

        public async Task<StandardResult> GetAllCourses()
        {
            var courses = await _course.GetAllCourses();


            var checkNullCourses = await _validate.CheckNullCourses(courses);
            if (checkNullCourses.Success == false) return checkNullCourses;

            var showCourses = new List<ShowCourseDto>();

            foreach (var item in courses)
            {
                var tempDto = await _mapper.MapAsync(item, new ShowCourseDto());
                showCourses.Add(tempDto);
            }

            var sr = new StandardResult<List<ShowCourseDto>>
            {
                Messages = new List<string> { "درس ها دریافت شدند" },
                Result = showCourses,
                Success = true,
                StatusCode = 200,
            };
            _logger.Error("SepehrAcademyApi : /GetAllCourses success:true");
            return sr;
        }

        public async Task<StandardResult> GetCourseById(int courseId)
        {
            var course = await _course.GetCourseById(courseId);

            var checkNullCourse = await _validate.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;

            var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

            var sr = new StandardResult<ShowCourseDto>
            {
                Messages = new List<string> { "درس با موفقیت دریافت شد" },
                Success = true,
                Result = showCourse,
                StatusCode = 200,
            };
            _logger.Error("SepehrAcademyApi : /GetCourseById success:true");
            return sr;

        }

        public async Task<StandardResult> UpdateCourse(int userId,int courseId, UpdateCourseDto dto)
        {
            var course = await _course.GetCourseById(courseId);

            var checkNullCourse = await _validate.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;


            var teacher = await _employee.GetEmployeeById(userId);
            var checkNullTeacher = await _validateEmployee.CheckNullEmployee(teacher);
            if (checkNullTeacher.Success == false) return checkNullTeacher;

            var checkTeacherForUpdateCourse = await _validate.CheckTeacherForUpdateCourse(teacher,course);
            if (checkTeacherForUpdateCourse.Success == false) return checkTeacherForUpdateCourse;

            course.CourseName = dto.CourseName;
            course.Description = dto.Description;
            course.Image = dto.Image;
            course.Topics = dto.Topics;

            _course.Save();

            var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

            var sr = new StandardResult<ShowCourseDto>
            {
                Messages = new List<string> { "درس با موفقیت ویرایش شد" },
                Success = true,
                Result = showCourse,
                StatusCode = 201,
            };
            _logger.Error("SepehrAcademyApi : /UpdateCourse success:true");
            return sr;
        }

        public async Task<StandardResult> UpdateCourseAdmin(int userId, int courseId, UpdateCourseAdminDto dto)
        {
            var course = await _course.GetCourseById(courseId);

            var checkNullCourse = await _validate.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;
            

            var admin = await _employee.GetEmployeeById(userId);
            var checkNullAdmin = await _validateEmployee.CheckNullEmployee(admin);
            if (checkNullAdmin.Success == false) return checkNullAdmin;

            var checkAdmin = await _validateEmployee.ChechAdmin(admin);
            if (checkAdmin.Success == false) return checkAdmin;


            var teacher = await _employee.GetEmployeeById(dto.TeacherId);
            var checkNullTeacher = await _validateEmployee.CheckNullTeacher(teacher);
            if (checkNullTeacher.Success == false) return checkNullTeacher;


            var courseTeacher = await _courseTeacher.GetCourseTeacher(course.Id, course.TeacherId);

            courseTeacher.Course = course;
            courseTeacher.CourseId = course.Id;
            courseTeacher.Teacher = teacher;
            courseTeacher.TeacherId = teacher.Id;


            course.CourseName = dto.CourseName;
            course.Description = dto.Description;
            course.Image = dto.Image;
            course.Topics = dto.Topics;
            course.TeacherId = teacher.Id;
            course.Teacher = teacher;
            
            _course.Save();


            var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

            var sr = new StandardResult<ShowCourseDto>
            {
                Messages = new List<string> { "درس با موفقیت ویرایش شد" },
                Success = true,
                Result = showCourse,
                StatusCode = 201,
            };
            _logger.Error("SepehrAcademyApi : /UpdateCourse success:true");
            return sr;
        }

        public async Task<StandardResult> DeleteCourse(int courseId)
        {
            var course = await _course.GetCourseById(courseId);

            var checkNullCourse = await _validate.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;

            course.IsDeleted = true;

            await _courseTeacher.Remove(course.Id, course.TeacherId);

            _course.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "درس با موفقیت حذف شد" },
                Success = true,
                StatusCode = 201,
            };
            _logger.Error("SepehrAcademyApi : /DeleteCourse success:true");
            return sr;
        }
    }
}
