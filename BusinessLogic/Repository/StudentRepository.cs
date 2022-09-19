using BusinessLogic.Contracts;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public StudentRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<List<StudentModel>> GetAllStudents()
        {
            return await _context.Students.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<StudentModel> GetStudentById(int id)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task<StudentModel> GetStudentByEmail(string email)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.Email == email && x.IsDeleted == false);
        }

        public async Task<bool> CheckPhoneNumber(string phoneNumber, int studentId)
        {
            return await _context.Employees.AnyAsync(x => x.NationalId == phoneNumber && x.Id != studentId);
        }

        public async Task<bool> CheckNationalId(string nationalId, int studentId)
        {
            return await _context.Employees.AnyAsync(x => x.NationalId == nationalId && x.Id != studentId);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
