using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public ContactUsRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(ContactUsModel contactUs)
        {
            await _context.AddAsync(contactUs);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ContactUsModel>> GetAllMessages()
        {
            return await _context.ContactUs.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<ContactUsModel> GetMessageById(int id)
        {
            return await _context.ContactUs.SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
