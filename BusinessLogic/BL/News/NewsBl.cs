using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Employee;
using BusinessLogic.Utils.News;
using DAL.DTO.News;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.News
{
    public class NewsBl : INewsBl
    {
        private readonly IEmployeeRepository _employee;
        private readonly INewsRepository _news;
        private readonly EmployeeValidation _validateEmployee;
        private readonly NewsValidation _validate;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly Mapper _mapper;

        public NewsBl(IEmployeeRepository employee, INewsRepository news, EmployeeValidation validateEmployee, NewsValidation validate, Mapper mapper)
        {
            _employee = employee;
            _news = news;
            _validateEmployee = validateEmployee;
            _validate = validate;
            _mapper = mapper;
        }


        public async Task<StandardResult> AddNews(int employeeId, AddNewsDto dto)
        {
            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validateEmployee.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;


            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.StatusCode == 404) return validateDto;

            var newNews = new NewsModel
            {
                Title = dto.Title,
                WriterId = employee.Id,
                WriterName = employee.FullName,
                Text = dto.Text,
                Image = dto.Image,
                Category = dto.Category
            };

            await _news.AddNews(newNews);


            var showNews = await _mapper.MapAsync(newNews, new ShowNewsDto());


            var sr = new StandardResult<ShowNewsDto>
            {
                Messages = new List<string> { "خبر ایجاد شد" },
                Result = showNews,
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /AddNews success:true");
            return sr;
        }

        public async Task<StandardResult> GetAllNews()
        {
            var allNews = await _news.GetAllNews();

            var checkNullNews = await _validate.CheckNullAllNews(allNews);
            if (checkNullNews.Success == false) return checkNullNews;

            var listAllNews = new List<ShowNewsDto>();

            foreach (var item in allNews)
            {
                var tempDto = await _mapper.MapAsync(item, new ShowNewsDto());
                listAllNews.Add(tempDto);
            }

            var sr = new StandardResult<List<ShowNewsDto>>
            {
                Messages = new List<string> { "خبر ها دریافت شدند" },
                Result = listAllNews,
                StatusCode = 200,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /GetAllNews success:true");
            return sr;

        }

        public async Task<StandardResult> GetNewsById(int newsId)
        {
            var news = await _news.GetNewsById(newsId);

            var checkNullNews = await _validate.CheckNullNews(news);
            if (checkNullNews.Success == false) return checkNullNews;

            var showNews = await _mapper.MapAsync(news, new ShowNewsDto());

            var sr = new StandardResult<ShowNewsDto>
            {
                Messages = new List<string> { "خبر دریافت شد" },
                Result = showNews,
                StatusCode = 200,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /GetNewsById success:true");
            return sr;

        }

        public async Task<StandardResult> UpdateNews(int employeeId, int newsId, UpdateNewsDto dto)
        {
            var news = await _news.GetNewsById(newsId);

            var checkNullNews = await _validate.CheckNullNews(news);
            if (checkNullNews.Success == false) return checkNullNews;


            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validateEmployee.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            if (employee.Role == "teacher")
            {
                var checkNewsWriter = await _validate.CheckNewsWriter(employee.Id, news);
                if (checkNewsWriter.Success == false) return checkNewsWriter;
            }

            news.Title = dto.Title;
            news.Category = dto.Category;
            news.Text = dto.Text;
            news.Image = dto.Image;

            _news.Save();

            var showUpdatedNews = await _mapper.MapAsync(news, new ShowNewsDto());

            var sr = new StandardResult<ShowNewsDto>
            {
                Messages = new List<string> { "خبر ویرایش شد" },
                Result = showUpdatedNews,
                StatusCode = 200,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /UpdateNews success:true");
            return sr;


        }

        public async Task<StandardResult> DeleteNews(int employeeId, int newsId)
        {
            var news = await _news.GetNewsById(newsId);

            var checkNullNews = await _validate.CheckNullNews(news);
            if (checkNullNews.Success == false) return checkNullNews;


            var employee = await _employee.GetEmployeeById(employeeId);

            var checkNullEmployee = await _validateEmployee.CheckNullEmployee(employee);
            if (checkNullEmployee.Success == false) return checkNullEmployee;

            if (employee.Role == "teacher")
            {
                var checkNewsWriter = await _validate.CheckNewsWriter(employee.Id, news);
                if (checkNewsWriter.Success == false) return checkNewsWriter;
            }

            news.IsDeleted = true;

            _news.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "خبر حذف شد" },
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /DeleteNews success:true");
            return sr;
        }
    }
}
