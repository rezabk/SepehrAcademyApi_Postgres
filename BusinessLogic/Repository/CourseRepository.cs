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
    public class CourseRepository : ICourseRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public CourseRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCourse(CourseModel course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CourseModel>> GetAllCourses()
        {
            return await _context.Courses.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<CourseModel> GetCourseById(int courseId)
        {
            return await _context.Courses.SingleOrDefaultAsync(x => x.Id == courseId && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
