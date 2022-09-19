using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repository
{
    public class TermStudentRepository : ITermStudentRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public TermStudentRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddStudentToTerm(TermStudentModel termStudentModel)
        {
            await _context.TermStudents.AddAsync(termStudentModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveStudentFromTerm(int studentId, int termId)
        {
            var res = await _context.TermStudents.SingleOrDefaultAsync(x =>
                x.StudentId == studentId && x.TermId == termId);
            _context.TermStudents.Remove(res);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> RemoveTermStudents(int termId)
        {
            var res = await _context.TermStudents.Where(x => x.TermId == termId).ToListAsync();
            _context.TermStudents.RemoveRange(res);
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckStudentAlreadyJoinedTerm(int studentId, int termId)
        {
            return await _context.TermStudents.AnyAsync(x => x.TermId == termId && x.StudentId == studentId);
        }

        public async Task<List<TermStudentModel>> TermStudents(int termId)
        {
            return await _context.TermStudents.Where(x => x.TermId == termId)
                .ToListAsync();
        }

        public async Task<List<TermStudentModel>> StudentTerms(int studentId)
        {
            return await _context.TermStudents.Where(x => x.StudentId == studentId).ToListAsync();
        }
    }
}
