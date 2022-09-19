using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Course;
using DAL.DTO.Lesson;
using DAL.Model;
using Data;

namespace BusinessLogic.BL.Lesson
{
    public interface ICourseBl
    {
        Task<StandardResult> AddCourse(int userId,AddCourseDto dto);

        Task<StandardResult> AddCourseAdmin(int userId, AddCourseAdminDto dto);

        Task<StandardResult> GetAllCourses( );

        Task<StandardResult> GetCourseById(int courseId);

        Task<StandardResult> UpdateCourse(int userId,int courseId,UpdateCourseDto dto);

        Task<StandardResult> UpdateCourseAdmin(int userId, int courseId, UpdateCourseAdminDto dto);

        Task<StandardResult> DeleteCourse(int courseId);
    }
}
