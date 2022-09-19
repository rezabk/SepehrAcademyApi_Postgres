using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Course;
using BusinessLogic.Utils.Employee;
using BusinessLogic.Utils.Lesson;
using BusinessLogic.Utils.Student;
using DAL.DTO.Course;
using DAL.DTO.Employee;
using DAL.DTO.Lesson;
using DAL.DTO.Student;
using DAL.Model;
using Data;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Serilog;

namespace BusinessLogic.BL.Course
{
    public class TermBl : ITermBl
    {
        private readonly ITermRepository _term;
        private readonly ITermStudentRepository _termStudent;
      
        private readonly TermValidation _validate;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly Mapper _mapper;
        private readonly IEmployeeRepository _employee;
        private readonly IStudentRepository _student;
        private readonly EmployeeValidation _validateEmployee;
        private readonly StudentValidation _validateStudent;
        private readonly ICourseRepository _course;
        private readonly CourseValidation _validateCourse;
        private readonly ShamasiCalendar _shamsi;

        public TermBl(ITermRepository term, ITermStudentRepository termStudent, TermValidation validate, Mapper mapper, IEmployeeRepository employee, IStudentRepository student, EmployeeValidation validateEmployee, StudentValidation validateStudent, ICourseRepository course, CourseValidation validateCourse, ShamasiCalendar shamsi)
        {
            _term = term;
            _termStudent = termStudent;
         
            _validate = validate;
            _mapper = mapper;
            _employee = employee;
            _student = student;
            _validateEmployee = validateEmployee;
            _validateStudent = validateStudent;
            _course = course;
            _validateCourse = validateCourse;
            _shamsi = shamsi;
        }

        public async Task<StandardResult> GetAllTerms()
        {
            var terms = await _term.GetAllTerms();
            var checkNullTerms = await _validate.CheckNullTerms(terms);
            if (checkNullTerms.Success == false) return checkNullTerms;


            var listTerms = new List<ShowTermDto>();
            foreach (var item in terms)
            {
                var termStudents = await _termStudent.TermStudents(item.Id);

                var students = new List<ShowStudentDto>();
                foreach (var i in termStudents)
                {
                    var student = await _student.GetStudentById(i.StudentId);
                    var studentDto = await _mapper.MapAsync(student, new ShowStudentDto());
                    students.Add(studentDto);
                }

                var course = await _course.GetCourseById(item.CourseId);
                var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

                var teacher = await _employee.GetEmployeeById(item.TeacherId);
                var showTeacher = await _mapper.MapAsync(teacher, new ShowTermTeacherDto());

                var tempDto = await _mapper.MapAsync(item, new ShowTermDto());
                tempDto.CourseDetails = showCourse;
                tempDto.StudentDetails = students;
                tempDto.TeacherDetails = showTeacher;

                listTerms.Add(tempDto);
            }

            var sr = new StandardResult<List<ShowTermDto>>
            {
                Messages = new List<string> { "دوره ها دریافت شدند" },
                Result = listTerms,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("FinalProject : /GetAllTerms success:true");
            return sr;


        }

        public async Task<StandardResult> GetTermById(int termId)
        {
            var term = await _term.GetTermById(termId);
            var checkNullTerm = await _validate.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;

            var TermStudents = await _termStudent.TermStudents(term.Id);

            var students = new List<ShowStudentDto>();
            foreach (var i in TermStudents)
            {
                var student = await _student.GetStudentById(i.StudentId);
                var studentDto = await _mapper.MapAsync(student, new ShowStudentDto());
                students.Add(studentDto);
            }

            var course = await _course.GetCourseById(term.CourseId);
            var showCourse = await _mapper.MapAsync(course, new ShowCourseDto());

            var teacher = await _employee.GetEmployeeById(term.TeacherId);
            var showTeacher = await _mapper.MapAsync(teacher, new ShowTermTeacherDto());


            var showTerm = await _mapper.MapAsync(term, new ShowTermDto());
            showTerm.CourseDetails = showCourse;
            showTerm.TeacherDetails = showTeacher;
            showTerm.StudentDetails = students;

            var sr = new StandardResult<ShowTermDto>
            {
                Messages = new List<string> { "دوره دریافت شد" },
                Result = showTerm,
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("FinalProject : /GetTermById success:true");
            return sr;

        }

        public async Task<StandardResult> CreateTerm(CreateTermDto dto)
        {
            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.Success == false) return validateDto;


            var teacher = await _employee.GetEmployeeById(dto.TeacherId);
            var checkNullTeacher = await _validateEmployee.CheckNullEmployee(teacher);
            if (checkNullTeacher.Success == false) return checkNullTeacher;


            var course = await _course.GetCourseById(dto.CourseId);
            var checkNullCourse = await _validateCourse.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;

            var checkActiveCourse = await _validateCourse.CheckActiveCourse(course);
            if (checkActiveCourse.Success == false) return checkActiveCourse;

            var newTerm = new TermModel
            {
                Title = dto.Title,
                Capacity = dto.Capacity,
                Duration = dto.Duration,
                Cost = dto.Cost,
                StartDate =dto.StartDate,
                EndDate = dto.EndDate,
                Teacher = teacher,
                TeacherId = dto.TeacherId,
                Course = course,
                CourseId = dto.CourseId,
                IsActive = true,
                IsDeleted = false,
            };

            await _term.CreateTerm(newTerm);


            var showTerm = await _mapper.MapAsync(newTerm, new ShowCreatedTermDto());

            var sr = new StandardResult<ShowCreatedTermDto>
            {
                Messages = new List<string> { "دوره با موفقیت ساخته شد" },
                Result = showTerm,
                StatusCode = 201,
                Success = true,
            };
            _logger.Information("FinalProject : /CreateTerm success:true");
            return sr;

        }

        public async Task<StandardResult> UpdateTerm(int termId, int employeeId, UpdateTermDto dto)
        {
            var term = await _term.GetTermById(termId);
            var checkNullTerm = await _validate.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;

            //var creatorTeacher = await _employee.GetEmployeeById(employeeId);
            //var checkTeacherForUpdate = await _validate.CheckTeacherForUpdateTerm(creatorTeacher, term);
            //if (checkTeacherForUpdate.Success == false) return checkTeacherForUpdate;

            var teacher = await _employee.GetEmployeeById(dto.TeacherId);
            var checkTeacher = await _validateEmployee.CheckNullEmployee(teacher);
            if (checkTeacher.Success == false) return checkTeacher;


            var course = await _course.GetCourseById(dto.CourseId);
            var checkNullCourse = await _validateCourse.CheckNullCourse(course);
            if (checkNullCourse.Success == false) return checkNullCourse;

            var checkActiveCourse = await _validateCourse.CheckActiveCourse(course);
            if (checkActiveCourse.Success == false) return checkActiveCourse;

            term.Title = dto.Title;
            term.Capacity = dto.Capacity;
            term.Duration = dto.Duration;
            term.Cost = dto.Cost;
            term.StartDate = dto.StartDate;
            term.EndDate = dto.EndDate;
            term.Teacher = teacher;
            term.TeacherId = dto.TeacherId;
            term.Course = course;
            term.CourseId = dto.CourseId;


     

            _term.Save();

            var showUpdatedTerm = await _mapper.MapAsync(dto, new ShowCreatedTermDto());

            var sr = new StandardResult<ShowCreatedTermDto>
            {
                Messages = new List<string> { "دوره با موفقیت ویرایش شد " },
                Result = showUpdatedTerm,
                StatusCode = 201,
                Success = true
            };
            _logger.Information("FinalProject : /UpdateTerm success:true");
            return sr;

        }

        public async Task<StandardResult> DeleteTerm(int termId)
        {
            var term = await _term.GetTermById(termId);
            var checkNullTerm = await _validate.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;

            await _termStudent.RemoveTermStudents(term.Id);
        

            term.IsDeleted = true;

            _term.Save();
            var sr = new StandardResult
            {
                Messages = new List<string> { "دوره حذف شد " },
                StatusCode = 201,
                Success = true
            };
            _logger.Information("FinalProject : /DeleteTerm success:true");
            return sr;
        }

        public async Task<StandardResult> AddStudentToTerm(int studentId, AddStudentToTermDto dto)
        {
            var student = await _student.GetStudentById(studentId);
            var checkNullStudent = await _validateStudent.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var term = await _term.GetTermById(dto.TermId);
            var checkNullTerm = await _validate.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;

            var checkStudentAlreadyJoinedTerm = await _validate.CheckStudentAlreadyJoinedTerm(studentId, term.Id);
            if (checkStudentAlreadyJoinedTerm.Success == false) return checkStudentAlreadyJoinedTerm;


            var newTermStudent = new TermStudentModel
            {
                Term = term,
                TermId = term.Id,
                Student = student,
                StudentId = student.Id
            };

            await _termStudent.AddStudentToTerm(newTermStudent);

            term.Students.Add(student);

            _term.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "دانشجو به دوره اضافه شد" },
                Success = true,
                StatusCode = 201,
            };
            _logger.Information("FinalProject : /AddStudentToTerm success:true");
            return sr;
        }

        public async Task<StandardResult> RemoveStudentFromTerm(int studentId, AddStudentToTermDto dto)
        {
            var student = await _student.GetStudentById(studentId);
            var checkNullStudent = await _validateStudent.CheckNullStudent(student);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var term = await _term.GetTermById(dto.TermId);
            var checkNullTerm = await _validate.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;

            var checkStudentJoinedTerm = await _validate.CheckStudentJoinedTerm(student.Id, term.Id);
            if (checkStudentJoinedTerm.Success == false) return checkStudentJoinedTerm;

            await _termStudent.RemoveStudentFromTerm(student.Id, term.Id);

            var sr = new StandardResult
            {
                Messages = new List<string> { "دانشجو از دوره حذف شد" },
                Success = true,
                StatusCode = 201,
            };
            _logger.Information("FinalProject : /RemoveStudentFromTerm success:true");
            return sr;

        }
    }
}
