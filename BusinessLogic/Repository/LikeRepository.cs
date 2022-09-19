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
    public class LikeRepository : ILikeRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public LikeRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> LikeTerm(LikeModel like)
        {
            await _context.likes.AddAsync(like);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisLikeTerm(int termId, int userId)
        {
            var res = await _context.likes.SingleOrDefaultAsync(x => x.TermId == termId && x.UserId == userId);
            _context.Remove(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckUserLikedTerm(int termId, int userId)
        {
            return await _context.likes.AnyAsync(x => x.TermId == termId && x.UserId == userId);
        }
    }
}
