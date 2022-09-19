using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ICommentNewsRepository
    {
        Task<bool> AddComment(CommentNewsModel comment);

        Task<List<CommentNewsModel>> GetAllComments();

        Task<CommentNewsModel> GetCommentById(int commentId);

        public void Save();
    }
}
