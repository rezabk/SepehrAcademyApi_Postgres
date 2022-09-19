using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.Register;
using DAL.DTO.Auth;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.Auth
{
    public class AuthBl : IAuthBl
    {
        private readonly IAuthRepository _auth;
        private readonly CreatePairToken _token;
        private readonly CreatePairTokenEmployee _tokenEmployee;
        private readonly HashPassword _hash;
        private readonly AuthValidation _validate;
        private readonly EmployeeAuthValidation _validateEmployee;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public AuthBl(IAuthRepository auth, CreatePairToken token, HashPassword hash, AuthValidation validate, CreatePairTokenEmployee tokenEmployee,
            EmployeeAuthValidation validateEmployee)
        {
            _auth = auth;
            _token = token;
            _hash = hash;
            _validate = validate;
            _tokenEmployee = tokenEmployee;
            _validateEmployee = validateEmployee;
        }

        public async Task<StandardResult> Login(LoginDto dto)
        {
            var validateDto = await _validate.ValidateLoginDto(dto);
            if (validateDto.StatusCode == 404) return validateDto;

            var hashPassword = _hash.GetHash(dto.Password);

            var student = await _auth.Login(dto.Email, hashPassword);

            if (student is null)
            {
                var er = new StandardResult<PairTokenDto>
                {
                    Messages = new List<string> { "اطلاعات ورودی اشتباه است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /Login success:false");
                return er;
            }

            var (tokens, refreshEx) = _token.GeneratePairToken(student);
            student.Refresh(tokens.RefreshToken, DateTime.UtcNow.AddDays(refreshEx));
            _auth.Save(); var sr = new StandardResult<PairTokenDto>
            {
                Messages = new List<string> { "با موفقیت وارد شدید" },
                Result = tokens,
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /Login success:true");
            return sr;


        }

        public async Task<StandardResult> LoginEmployee(LoginDto dto)
        {
            var validateDto = await _validateEmployee.ValidateLoginDto(dto);
            if (validateDto.StatusCode == 404) return validateDto;

            var hashPassword = _hash.GetHash(dto.Password);

            var employee = await _auth.LoginEmployee(dto.Email, hashPassword);

            if (employee is null)
            {
                var er = new StandardResult<PairTokenDto>
                {
                    Messages = new List<string> { "اطلاعات ورودی اشتباه است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /LoginEmployee success:false");
                return er;
            }

            var (tokens, refreshEx) = _tokenEmployee.GeneratePairToken(employee);
            employee.Refresh(tokens.RefreshToken, DateTime.UtcNow.AddDays(refreshEx));
            _auth.Save(); var sr = new StandardResult<PairTokenDto>
            {
                Messages = new List<string> { "با موفقیت وارد شدید" },
                Result = tokens,
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /Login success:true");
            return sr;


        }

        public async Task<StandardResult> Register(RegisterDto dto)
        {

            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.StatusCode == 404) return validateDto;

            var checkInfoForRegister = await _validate.CheckInfoForRegister(dto);
            if (checkInfoForRegister.Success == false) return checkInfoForRegister;


            var newStudent = new StudentModel
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = _hash.GetHash(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                Role = "student",
                NationalId = dto.NationalId,
                BirthDate = dto.BirthDate,
                IsActive = true,
                IsDeleted = false,
            };
            await _auth.Register(newStudent);
            var (tokens, refreshEx) = _token.GeneratePairToken(newStudent);
            newStudent.Refresh(tokens.RefreshToken, DateTime.UtcNow.AddDays(refreshEx));
            _auth.Save();


            var sr = new StandardResult<PairTokenDto>
            {
                Messages = new List<string> { "ثبت نام انجام شد" },
                Result = tokens,
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /Register success:true");
            return sr;


        }

        public async Task<StandardResult> RegisterEmployee(RegisterDto dto)
        {
            var validateDto = await _validateEmployee.ValidateDto(dto);
            if (validateDto.StatusCode == 404) return validateDto;

            var checkInfoForRegister = await _validateEmployee.CheckInfoForRegisterEmployee(dto);
            if (checkInfoForRegister.Success == false) return checkInfoForRegister;


            var newEmployee = new EmployeeModel
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = _hash.GetHash(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                Role = "teacher",
                NationalId = dto.NationalId,
                BirthDate = dto.BirthDate,
                IsActive = true,
                IsDeleted = false,
            };
            await _auth.RegisterEmployee(newEmployee);
            var (tokens, refreshEx) = _tokenEmployee.GeneratePairToken(newEmployee);
            newEmployee.Refresh(tokens.RefreshToken, DateTime.UtcNow.AddDays(refreshEx));
            _auth.Save();


            var sr = new StandardResult<PairTokenDto>
            {
                Messages = new List<string> { "ثبت نام انجام شد" },
                Result = tokens,
                StatusCode = 201,
                Success = true,

            };
            _logger.Information("SepehrAcademyApi : /RegisterEmployee success:true");
            return sr;
        }
    }
}
