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
    public class CommentRepository : ICommentRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public CommentRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddComment(CommentModel comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CommentModel>> GetAllComments()
        {
            return await _context.Comments.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<CommentModel> GetCommentById(int commentId)
        {
            return await _context.Comments.SingleOrDefaultAsync(x => x.Id == commentId && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
