using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IStudentRepository
    {
        Task<List<StudentModel>> GetAllStudents();

        Task<StudentModel> GetStudentById(int id);


        Task<StudentModel> GetStudentByEmail(string email);
        Task<bool> CheckPhoneNumber(string phoneNumber, int studentId);

        Task<bool> CheckNationalId(string nationalId, int studentId);


        public void Save();
    }
}
