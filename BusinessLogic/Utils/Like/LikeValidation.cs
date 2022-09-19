using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Like
{
    public class LikeValidation
    {
        private readonly ILikeRepository _like;
        private readonly Serilog.ILogger _logger = Log.Logger;
        public LikeValidation(ILikeRepository like)
        {
            _like = like;
        }
        public async Task<StandardResult> CheckUserLikedCourse(int courseId,int userId )
        {
            var checkUserLikedTerm = await _like.CheckUserLikedTerm(courseId, userId);
            if (checkUserLikedTerm)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "دوره را قبلا لایک کردید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /LikeTerm success:false");
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

        public async Task<StandardResult> CheckLike(int termId, int userId)
        {
            var checkUserLikedTerm = await _like.CheckUserLikedTerm(termId, userId);
            if (checkUserLikedTerm == false)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این دوره را لایک نکردید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /LikeTerm success:false");
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
