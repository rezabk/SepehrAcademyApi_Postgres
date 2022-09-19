using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface IContactUsRepository
    {
        Task<bool> Add(ContactUsModel contactUs);

        Task<List<ContactUsModel>> GetAllMessages();


        Task<ContactUsModel> GetMessageById(int id);
        public void Save();
    }
}
