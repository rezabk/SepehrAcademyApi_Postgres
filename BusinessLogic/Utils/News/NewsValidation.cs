using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.News;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.News
{
    public class NewsValidation
    {
        private readonly Serilog.ILogger _logger = Log.Logger;
        public async Task<StandardResult> ValidateDto(AddNewsDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا عنوان خبر را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /AddNews success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Category))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا دسته بندی خبر را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /AddNews success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا متن خبر  را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /AddNews success:false");
                return er;
            }


            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }


        public async Task<StandardResult> CheckNullAllNews(List<NewsModel> allNews)
        {
            if (allNews.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ خبری وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetAllNews success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }

        public async Task<StandardResult> CheckNullNews(NewsModel news)
        {
            if (news is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { " خبری با آیدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetNewsById success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }

        public async Task<StandardResult> CheckNewsWriter(int employeeId, NewsModel news)
        {
            if (employeeId != news.WriterId)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { " شما نویسنده این خبر نیستید" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /UpdateNews success:false");
                return er;
            }
            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }




    }
}
