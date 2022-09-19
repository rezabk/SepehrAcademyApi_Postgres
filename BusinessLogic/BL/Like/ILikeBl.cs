using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.Like;
using Data;

namespace BusinessLogic.BL.Like
{
    public interface ILikeBl
    {
        Task<StandardResult> LikeTerm(LikeDto dto);

        Task<StandardResult> DisLikeTerm(LikeDto dto);

        Task<StandardResult> CheckUserLikedThisTerm(CheckLikeDto dto);
    }
}
