using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Student;

namespace BusinessLogic.BL.Student
{
    public interface IStudentBl
    {
        Task<StandardResult> GetAllStudents();

        Task<StandardResult> GetStudentById(int studentId);

        Task<StandardResult> UpdateStudent(int studentId,UpdateStudentDto dto);

        Task<StandardResult> DeleteStudent(int studentId );

        Task<StandardResult> ActiveStudent(int studentId);

        Task<StandardResult> DeActiveStudent(int studentId);
    }
}
