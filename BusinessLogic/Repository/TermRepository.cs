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
    public class TermRepository : ITermRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public TermRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<List<TermModel>> GetAllTerms()
        {
            return await _context.Terms.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<bool> CreateTerm(TermModel term)
        {
            await _context.Terms.AddAsync(term);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TermModel> GetTermById(int termId)
        {
            return await _context.Terms.SingleOrDefaultAsync(x => x.Id == termId && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
