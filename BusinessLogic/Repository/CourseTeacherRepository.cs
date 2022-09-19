using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repository
{
    public class CourseTeacherRepository : ICourseTeacherRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public CourseTeacherRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(CourseTeacherModel courseTeacher)
        {
            await _context.CourseTeachers.AddAsync(courseTeacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(int courseId, int teacherId)
        {
            var res = await _context.CourseTeachers.SingleOrDefaultAsync(x =>
                x.CourseId == courseId && x.TeacherId == teacherId);
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CourseTeacherModel> GetCourseTeacher(int courseId, int teacherId)
        {
            return await _context.CourseTeachers.SingleOrDefaultAsync(x =>
                x.CourseId == courseId && x.TeacherId == teacherId);
        }
    }
}
