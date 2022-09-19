using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Student;
using DAL.DTO.Student;
using DAL.Model;
using Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Utils.Register;
using DAL.DTO.Course;
using DAL.DTO.Lesson;

namespace BusinessLogic.BL.Student
{
    public class StudentBl : IStudentBl
    {
        private readonly IStudentRepository _student;
        private readonly ITermStudentRepository _termStudent;
        private readonly ICourseRepository _course;
        private readonly IEmployeeRepository _employee;
        private readonly ITermRepository _term;
        private readonly HashPassword _hash;
        private readonly Mapper _mapper;
        private readonly StudentValidation _validate;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public StudentBl(IStudentRepository student, ITermStudentRepository termStudent, ICourseRepository course, IEmployeeRepository employee, ITermRepository term, HashPassword hash, Mapper mapper, StudentValidation validate)
        {
            _student = student;
            _termStudent = termStudent;
            _course = course;
            _employee = employee;
            _term = term;
            _hash = hash;
            _mapper = mapper;
            _validate = validate;
        }
        public async Task<StandardResult> GetAllStudents()
        {
            var students = await _student.GetAllStudents();

            var checkNullStudents = await _validate.CheckNullStudents(students);
            if (checkNullStudents.Success == false) return checkNullStudents;

            var listStudents = new List<ShowStudentDto>();

            foreach (var item in students)
            {

                var terms = await _termStudent.StudentTerms(item.Id);

                var listTerms = new List<ShowStudentTermsDto>();
                foreach (var i in terms)
                {
                    var term = await _term.GetTermById(i.TermId);

                    var course = await _course.GetCourseById(term.CourseId);
                    var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

                    var teacher = await _employee.GetEmployeeById(term.TeacherId);
                    var showTeacher = await _mapper.MapAsync(teacher, new ShowTermTeacherDto());

                    var termsDto = await _mapper.MapAsync(term, new ShowStudentTermsDto());
                    termsDto.CourseDetails = showCourse;
                    termsDto.TeacherDetails = showTeacher;

                    listTerms.Add(termsDto);
                }

                var tempDto = await _mapper.MapAsync(item, new ShowStudentDto());
                tempDto.TermDetails = listTerms;
                listStudents.Add(tempDto);
            }
            var sr = new StandardResult<List<ShowStudentDto>>
            {
                Messages = new List<string> { "دانش آموزان دریافت شدند" },
                Result = listStudents,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /GetAllStudents success:true");
            return sr;

        }

        public async Task<StandardResult> GetStudentById(int studentId)
        {
            var student = await _student.GetStudentById(studentId);

            var checkNullStudent = await _validate.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var courses = await _termStudent.StudentTerms(student.Id);

            var listTerms = new List<ShowStudentTermsDto>();
            foreach (var i in courses)
            {
                var term = await _term.GetTermById(i.TermId);

                var course = await _course.GetCourseById(term.CourseId);
                var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

                var teacher = await _employee.GetEmployeeById(term.TeacherId);
                var showTeacher = await _mapper.MapAsync(teacher, new ShowTermTeacherDto());

                var termsDto = await _mapper.MapAsync(term, new ShowStudentTermsDto());
                termsDto.CourseDetails = showCourse;
                termsDto.TeacherDetails = showTeacher;

                listTerms.Add(termsDto);
            }


            var showStudent = await _mapper.MapAsync(student, new ShowStudentDto());
            showStudent.TermDetails = listTerms;
            var sr = new StandardResult<ShowStudentDto>

            {
                Messages = new List<string> { "دانش آموزان دریافت شدند" },
                Result = showStudent,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /GetStudentById success:true");
            return sr;

        }

        public async Task<StandardResult> UpdateStudent(int studentId, UpdateStudentDto dto)
        {
            var student = await _student.GetStudentById(studentId);

            var checkNullStudent = await _validate.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkInfo = await _validate.CheckValidateInfo(studentId, dto);
            if (checkInfo.Success == false) return checkInfo;


            student.FullName = dto.FullName;
            student.PhoneNumber = dto.PhoneNumber;
            student.BirthDate = dto.BirthDate;
            student.Address = dto.Address;
            student.NationalId = dto.NationalId;
            student.Profile = dto.Profile;

            _student.Save();

            var showUpdatedUser = await _mapper.MapAsync(dto, new ShowStudentDto());
            showUpdatedUser.Email = student.Email;
            showUpdatedUser.Id = student.Id;
            showUpdatedUser.Profile = student.Profile;
            showUpdatedUser.Role = student.Role;

            var sr = new StandardResult<ShowStudentDto>

            {
                Messages = new List<string> { "اطلاعات آپدیت شد" },
                Result = showUpdatedUser,
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /UpdateStudent success:true");
            return sr;
        }

        public async Task<StandardResult> DeleteStudent(int studentId)
        {
            var student = await _student.GetStudentById(studentId);

            var checkNullStudent = await _validate.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            student.IsDeleted = true;

            _student.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { "دانش آموز پاک شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /DeleteStudent success:true");
            return sr;
        }

        public async Task<StandardResult> ActiveStudent(int studentId)
        {
            var student = await _student.GetStudentById(studentId);

            var checkNullStudent = await _validate.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkActiveStudent = await _validate.CheckActiveStudent(student);
            if (checkActiveStudent.Success == false) return checkActiveStudent;

            student.IsActive = true;

            _student.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { "دانش آموز فعال شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /ActiveStudent success:true");
            return sr;
        }

        public async Task<StandardResult> DeActiveStudent(int studentId)
        {
            var student = await _student.GetStudentById(studentId);

            var checkNullStudent = await _validate.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkDeActiveStudent = await _validate.CheckDeActiveStudent(student);
            if (checkDeActiveStudent.Success == false) return checkDeActiveStudent;

            student.IsActive = false;

            _student.Save();

            var sr = new StandardResult

            {
                Messages = new List<string> { "دانش آموز غیر فعال شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("SepehrAcademyApi : /DeActiveStudent success:true");
            return sr;
        }
    }
}
