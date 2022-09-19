using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Comment;
using BusinessLogic.Utils.Lesson;
using BusinessLogic.Utils.Student;
using DAL.DTO.Comment;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.Comment
{
    public class CommentBl : ICommentBl
    {
        private readonly ICommentRepository _comment;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly CommentValidation _validate;
        private readonly IStudentRepository _student;
        private readonly StudentValidation _validateStudent;
        private readonly Mapper _mapper;

        public CommentBl(ICommentRepository comment, CommentValidation validate, IStudentRepository student, StudentValidation validateStudent, Mapper mapper)
        {
            _comment = comment;
            _validate = validate;
            _student = student;
            _validateStudent = validateStudent;
            _mapper = mapper;
        }
        public async Task<StandardResult> SendComment(int userId, SendCommentDto dto)
        {
            var user = await _student.GetStudentById(userId);
            var checkNullUser = await _validateStudent.CheckNullStudent(user);
            if (checkNullUser.Success == false) return checkNullUser;

            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.Success == false) return validateDto;

            var newComment = new CommentModel
            {
                Comment = dto.Comment,
                Email = user.Email,
                PostId = dto.PostId,
                IsDeleted = false,
                IsVerified = false,
            };
            await _comment.AddComment(newComment);

            var showComment = await _mapper.MapAsync(newComment, new ShowCommentDto());

            var sr = new StandardResult<ShowCommentDto>
            {
                Messages = new List<string> { "کامنت ثبت شد" },
                Result = showComment,
                StatusCode = 201,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /SendComment success:true");
            return sr;
        }

        public async Task<StandardResult> GetAllComments()
        {
            var comments = await _comment.GetAllComments();

            var checkNullComments = await _validate.CheckNullComments(comments);
            if (checkNullComments.Success == false) return checkNullComments;

            var listComments = new List<ShowCommentDto>();

            foreach (var item in comments)
            {
                var tempDto = await _mapper.MapAsync(item, new ShowCommentDto());
                listComments.Add(tempDto);
            }

            var sr = new StandardResult<List<ShowCommentDto>>
            {
                Messages = new List<string> { "کامت ها دریافت شدند" },
                Result = listComments,
                StatusCode = 200,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /GetAllComments success:true");
            return sr;
        }

        public async Task<StandardResult> VerifyComment(VerifyCommentDto dto)
        {
            var comment = await _comment.GetCommentById(dto.Id);

            var checkNullComment = await _validate.CheckNullComment(comment);
            if (checkNullComment.Success == false) return checkNullComment;

            comment.IsVerified = true;

            _comment.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "کامنت تایید شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /VerifyComment success:true");
            return sr;

        }
    }
}
