using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ILikeRepository
    {
        Task<bool> LikeTerm(LikeModel like);

        Task<bool> DisLikeTerm(int termId, int userId);

        Task<bool> CheckUserLikedTerm(int termId, int userId);
    }
}
