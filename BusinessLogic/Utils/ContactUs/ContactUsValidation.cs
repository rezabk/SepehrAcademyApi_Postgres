using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO.ContactUs;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.ContactUs
{
    public class ContactUsValidation
    {
        private readonly Serilog.ILogger _logger = Log.Logger;
        public async Task<StandardResult> ValidateDto(SendMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا درخواست را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /ValidateDto success:false");
                return er;
            }
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا نام خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /ValidateDto success:false");
                return er;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا نام خود را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /ValidateDto success:false");
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

        public async Task<StandardResult> CheckNullMessages(List<ContactUsModel> messages)
        {
            if (messages.Count == 0)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "هیچ پیغامی وجود ندارد" },
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullMessages success:false");
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

        public async Task<StandardResult> CheckNullMessage(ContactUsModel message)
        {
            if (message is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "پیغامی با ایدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckNullMessage success:false");
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

        public async Task<StandardResult> CheckVerifiedMessage(ContactUsModel message)
        {
            if (message.IsVerified == true)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "پیغام تایید شده است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckVerifiedMessage success:false");
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

        public async Task<StandardResult> CheckDeletedMessage(ContactUsModel message)
        {
            if (message.IsDeleted)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "پیغام حذف شده است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CheckVerifiedMessage success:false");
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
