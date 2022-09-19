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
    public class NewsRepository : INewsRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public NewsRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddNews(NewsModel news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NewsModel>> GetAllNews()
        {
            return await _context.News.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<NewsModel> GetNewsById(int newsId)
        {
            return await _context.News.SingleOrDefaultAsync(x => x.Id == newsId && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
