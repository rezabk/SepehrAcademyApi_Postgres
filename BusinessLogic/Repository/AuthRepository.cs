using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL;
using DAL.DTO.Auth;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SepehrAcademyDbContext _context;

        public AuthRepository(SepehrAcademyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Register(StudentModel student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StudentModel> Login(string email, string password)
        {
            return await _context.Students.SingleOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsDeleted == false);
        }

        public async Task<bool> CheckEmailForRegister(string email)
        {
            return await _context.Students.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> CheckPhoneNumberForRegister(string phoneNumber)
        {
            return await _context.Students.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<bool> CheckNationalIdForRegister(string nationalId)
        {
            return await _context.Students.AnyAsync(x => x.NationalId == nationalId);
        }





        public async Task<bool> RegisterEmployee(EmployeeModel employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<EmployeeModel> LoginEmployee(string email, string password)
        {
            return await _context.Employees.SingleOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsDeleted == false);
        }

        public async Task<bool> CheckEmailForRegisterEmployee(string email)
        {
            return await _context.Employees.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> CheckPhoneNumberForRegisterEmployee(string phoneNumber)
        {
            return await _context.Employees.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<bool> CheckNationalIdForRegisterEmployee(string nationalId)
        {
            return await _context.Employees.AnyAsync(x => x.NationalId == nationalId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
