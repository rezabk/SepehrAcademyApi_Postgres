using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ICommentRepository
    {
        Task<bool> AddComment(CommentModel comment);

        Task<List<CommentModel>> GetAllComments();

        Task<CommentModel> GetCommentById(int commentId);

        public void Save();
    }
}
