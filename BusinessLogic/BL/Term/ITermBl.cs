using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Course;
using Data;

namespace BusinessLogic.BL.Course
{
    public interface ITermBl
    {
        Task<StandardResult> GetAllTerms( );

        Task<StandardResult> GetTermById(int courseId);

        Task<StandardResult> CreateTerm(CreateTermDto dto);

        Task<StandardResult> UpdateTerm(int termId,int employeeId,UpdateTermDto dto);

        Task<StandardResult> DeleteTerm(int termId);

        Task<StandardResult> AddStudentToTerm(int studentId, AddStudentToTermDto dto);

        Task<StandardResult> RemoveStudentFromTerm(int studentId, AddStudentToTermDto dto);
    }
}
