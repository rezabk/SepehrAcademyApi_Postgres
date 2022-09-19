using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using BusinessLogic.Utils.Course;
using BusinessLogic.Utils.Like;
using BusinessLogic.Utils.Student;
using DAL.DTO.Like;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.Like
{
    public class LikeBl : ILikeBl
    {
        private readonly LikeValidation _validate;
        private readonly ILikeRepository _like;
        private readonly ITermRepository _term;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly TermValidation _validateTerm;
        private readonly IStudentRepository _student;
        private readonly StudentValidation _validateStudent;

        public LikeBl(LikeValidation validate, ILikeRepository like, ITermRepository term, TermValidation validateTerm, IStudentRepository student, StudentValidation validateStudent)
        {
            _validate = validate;
            _like = like;
            _term = term;
            _validateTerm = validateTerm;
            _student = student;
            _validateStudent = validateStudent;
        }

        public async Task<StandardResult> LikeTerm(LikeDto dto)
        {
            var term = await _term.GetTermById(dto.TermId);
            var checkNullTerm = await _validateTerm.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;


            var user = await _student.GetStudentById(dto.UserId);
            var checkNullStudent = await _validateStudent.CheckNullStudent(user);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkUserLikedTerm = await _validate.CheckUserLikedCourse(term.Id, user.Id);
            if (checkUserLikedTerm.Success == false) return checkUserLikedTerm;

            var newLike = new LikeModel
            {
                TermId = term.Id,
                UserId = user.Id,
            };

            await _like.LikeTerm(newLike);


            var sr = new StandardResult
            {
                Messages = new List<string> { "دوره لایک شد" },
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("FinalProject : /LikeTerm success:true");
            return sr;
        }

        public async Task<StandardResult> DisLikeTerm(LikeDto dto)
        {
            var term = await _term.GetTermById(dto.TermId);
            var checkNullTerm = await _validateTerm.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;


            var user = await _student.GetStudentById(dto.UserId);
            var checkNullStudent = await _validateStudent.CheckNullStudent(user);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkLike = await _validate.CheckLike(term.Id, user.Id);
            if (checkLike.Success == false) return checkLike;

            await _like.DisLikeTerm(term.Id, user.Id);

            var sr = new StandardResult
            {
                Messages = new List<string> { "دوره دیس لایک شد" },
                StatusCode = 200,
                Success = true,
            };
            _logger.Information("FinalProject : /DisLikeTerm success:true");
            return sr;
        }

        public async Task<StandardResult> CheckUserLikedThisTerm(CheckLikeDto dto)
        {
            var term = await _term.GetTermById(dto.TermId);
            var checkNullTerm = await _validateTerm.CheckNullTerm(term);
            if (checkNullTerm.Success == false) return checkNullTerm;


            var user = await _student.GetStudentById(dto.UserId);
            var checkNullStudent = await _validateStudent.CheckNullStudent(user);
            if (checkNullStudent.Success == false) return checkNullStudent;

            var checkLike = await _validate.CheckLike(term.Id, user.Id);

            if (checkLike.Success)
            {
                var sr = new StandardResult
                {
                    Messages = new List<string> { "Liked" },
                    StatusCode = 200,
                    Success = true,
                };
                _logger.Information("FinalProject : /CheckUserLikedThisTerm success:true");
                return sr;
            }
            var er = new StandardResult
            {
                Messages = new List<string> { "NotLiked" },
                StatusCode = 200,
                Success = false,
            };
            _logger.Information("FinalProject : /CheckUserLikedThisTerm success:true");
            return er;

        }
    }
}
