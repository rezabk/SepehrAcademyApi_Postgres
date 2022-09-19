using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Comment;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Comment
{
    public class CommentValidation
    {
        private readonly ICommentRepository _comment;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public CommentValidation(ICommentRepository comment)
        {
            _comment = comment;
        }

        public async Task<StandardResult> ValidateDto(SendCommentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Comment))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا متن کامنت را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /ValidateDto success:false");
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




        public async Task<StandardResult> CheckNullComments(List<CommentModel> comments)
        {
            if (comments.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ کامنتی وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetAllComments success:false");
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

        public async Task<StandardResult> CheckNullComment(CommentModel comment)
        {
            if (comment is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "کامنتی با آیدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetCommentById success:false");
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
