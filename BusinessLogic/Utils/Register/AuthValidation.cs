using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Auth;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Register
{
    public class AuthValidation
    {
        private readonly IAuthRepository _auth;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public AuthValidation(IAuthRepository auth)
        {
            _auth = auth;
        }

        public async Task<StandardResult> ValidateDto(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا نام خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا ایمیل خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا شماره موبایل خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا رمز عبور خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }


        public async Task<StandardResult> CheckInfoForRegister(RegisterDto dto)
        {
            var checkEmailForRegister = await _auth.CheckEmailForRegister(dto.Email);
            if (checkEmailForRegister)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این ایمیل قبلا در سایت ثبت نام کرده است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            var checkPhoneNumberForRegister = await _auth.CheckPhoneNumberForRegister(dto.PhoneNumber);
            if (checkPhoneNumberForRegister)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این شماره موبایل قبلا در سایت ثبت نام کرده است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }


            var checkNationalIdForRegister = await _auth.CheckNationalIdForRegister(dto.NationalId);
            if (checkNationalIdForRegister)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "این کد ملی در سایت موجود است" },
                    StatusCode = 404,
                    Success = false,

                };
                _logger.Error("SepehrAcademyApi : /Register success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبار سنجی انجام شد" },
                StatusCode = 200,
                Success = true,

            };

            return sr;

        }

        public async Task<StandardResult> ValidateLoginDto(LoginDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا ایمیل خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Login success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا رمز عبور خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /Login success:false");
                return er;
            }

            var sr = new StandardResult
            {
                Messages = new List<string> { "اعتبارسنجی انجام شد" },
                StatusCode = 200,
                Success = true,
            };

            return sr;
        }

    }
}
