using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.ContactUs;
using Data;

namespace BusinessLogic.BL.ContactUs
{
    public interface IContactUsBl
    {
        Task<StandardResult> SendMessage(SendMessageDto dto);

        Task<StandardResult> GetAllMessages();

        Task<StandardResult> VerifyMessage(int id);

        Task<StandardResult> DeleteMessage(int id);
    }
}
