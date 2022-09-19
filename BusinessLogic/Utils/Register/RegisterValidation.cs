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
    public class RegisterValidation
    {
        private readonly IAuthRepository _auth;
        private readonly Serilog.ILogger _logger = Log.Logger;

        public RegisterValidation(IAuthRepository auth)
        {
            _auth = auth;
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
    }
}
