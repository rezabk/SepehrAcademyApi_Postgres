using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ICourseTeacherRepository
    {
        Task<bool> Add(CourseTeacherModel courseTeacher);

        Task<bool> Remove(int courseId, int teacherId);

        Task<CourseTeacherModel> GetCourseTeacher(int courseId, int teacherId);
    }
}
