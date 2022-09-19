using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.News;
using DAL.Model;
using Data;

namespace BusinessLogic.BL.News
{
    public interface INewsBl
    {
        Task<StandardResult> AddNews(int employeeId, AddNewsDto dto);

        Task<StandardResult> GetAllNews();

        Task<StandardResult> GetNewsById(int newsId);

        Task<StandardResult> UpdateNews(int employeeId,int newsId,UpdateNewsDto dto);

        Task<StandardResult> DeleteNews(int employeeId, int newsId);

    }
}
