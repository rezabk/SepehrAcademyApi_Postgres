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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public EmployeeRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            return await _context.Employees.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<EmployeeModel>> GetAllTeachers()
        {
            return await _context.Employees.Where(x => x.Role == "teacher" && x.IsDeleted == false).ToListAsync();
        }

        public async Task<EmployeeModel> GetEmployeeById(int employeeId)
        {
            return await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId && x.IsDeleted == false);
        }

        public async Task<bool> CheckPhoneNumber(string phoneNumber, int employeeId)
        {
            return await _context.Employees.AnyAsync(x => x.PhoneNumber == phoneNumber && x.Id != employeeId);
        }

        public async  Task<bool> CheckNationalId(string nationalId, int employeeId)
        {
            return await _context.Employees.AnyAsync(x => x.NationalId == nationalId && x.Id != employeeId);
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
