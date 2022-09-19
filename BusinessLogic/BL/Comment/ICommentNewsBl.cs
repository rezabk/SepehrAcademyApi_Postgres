using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Comment;
using Data;

namespace BusinessLogic.BL.Comment
{
    public interface ICommentNewsBl
    {
        Task<StandardResult> SendComment(int userId,SendCommentDto dto);

        Task<StandardResult> GetAllComments();

        Task<StandardResult> VerifyComment(VerifyCommentDto dto);
    }
}
