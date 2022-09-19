using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface INewsRepository
    {
        Task<bool> AddNews(NewsModel news);

        Task<List<NewsModel>> GetAllNews();

        Task<NewsModel> GetNewsById(int newsId);

        public void Save();
    }
}
