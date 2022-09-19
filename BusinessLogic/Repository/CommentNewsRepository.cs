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
    public class CommentNewsRepository : ICommentNewsRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public CommentNewsRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddComment(CommentNewsModel comment)
        {
            await _context.CommentNews.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CommentNewsModel>> GetAllComments()
        {
            return await _context.CommentNews.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<CommentNewsModel> GetCommentById(int commentId)
        {
            return await _context.CommentNews.SingleOrDefaultAsync(x => x.Id == commentId && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
