using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ICourseRepository
    {
        Task<bool> AddCourse(CourseModel course);

        Task<List<CourseModel>> GetAllCourses();

        Task<CourseModel> GetCourseById(int courseId);

        public void Save();
    }
}
