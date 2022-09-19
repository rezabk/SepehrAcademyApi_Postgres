using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ITermStudentRepository
    {
        Task<bool> AddStudentToTerm(TermStudentModel termStudentModel);

        Task<bool> RemoveStudentFromTerm(int sutdentId, int termId);

        Task<bool> RemoveTermStudents(int courseId);

        Task<bool> CheckStudentAlreadyJoinedTerm(int studentId, int termId);

        Task<List<TermStudentModel>> TermStudents(int termId);

        Task<List<TermStudentModel>> StudentTerms(int studentId);
    }
}
