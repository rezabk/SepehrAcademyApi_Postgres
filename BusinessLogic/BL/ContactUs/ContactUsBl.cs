using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using BusinessLogic.Utils;
using BusinessLogic.Utils.ContactUs;
using BusinessLogic.Utils.Student;
using DAL.DTO.ContactUs;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.BL.ContactUs
{
    public class ContactUsBl : IContactUsBl
    {
        private readonly IContactUsRepository _contactUs;
        private readonly IStudentRepository _student;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly ContactUsValidation _validate;
        private readonly StudentValidation _validateStudent;
        private readonly Mapper _mapper;

        public ContactUsBl(IContactUsRepository contactUs, IStudentRepository student, ContactUsValidation validate, StudentValidation validateStudent, Mapper mapper)
        {
            _contactUs = contactUs;
            _student = student;
            _validate = validate;
            _validateStudent = validateStudent;
            _mapper = mapper;
        }

        public async Task<StandardResult> SendMessage(SendMessageDto dto)
        {

            var validateDto = await _validate.ValidateDto(dto);
            if (validateDto.Success == false) return validateDto;


            var newMessage = new ContactUsModel
            {
                Email = dto.Email,
                FullName = dto.FullName,
                IsDeleted = false,
                IsVerified = false,
                Message = dto.Message,
            };
            await _contactUs.Add(newMessage);

            var sr = new StandardResult
            {
                Messages = new List<string> { "درخواست شما با موفقیت ثبت شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /SendMessage success:true");
            return sr;
        }

        public async Task<StandardResult> GetAllMessages()
        {
            var messages = await _contactUs.GetAllMessages();

            var checkNullMessages = await _validate.CheckNullMessages(messages);
            if (checkNullMessages.Success == false) return checkNullMessages;

            var sr = new StandardResult<List<ContactUsModel>>
            {
                Messages = new List<string> { "پیغام ها دریافت شدند" },
                StatusCode = 200,
                Success = true,
                Result = messages,
            };
            _logger.Error("SepehrAcademyApi : /GetAllMessages success:true");
            return sr;
        }

        public async Task<StandardResult> VerifyMessage(int id)
        {
            var message = await _contactUs.GetMessageById(id);
            var checkNullMessage = await _validate.CheckNullMessage(message);
            if (checkNullMessage.Success == false) return checkNullMessage;

            var checkVerifiedMessage = await _validate.CheckVerifiedMessage(message);
            if (checkVerifiedMessage.Success == false) return checkVerifiedMessage;

            message.IsVerified = true;
            _contactUs.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "پیغام تایید شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /VerifyMessage success:true");
            return sr;
        }

        public async Task<StandardResult> DeleteMessage(int id)
        {
            var message = await _contactUs.GetMessageById(id);
            var checkNullMessage = await _validate.CheckNullMessage(message);
            if (checkNullMessage.Success == false) return checkNullMessage;

            var checkDeletedMessage = await _validate.CheckDeletedMessage(message);
            if (checkDeletedMessage.Success == false) return checkDeletedMessage;

            message.IsDeleted = true;
            _contactUs.Save();

            var sr = new StandardResult
            {
                Messages = new List<string> { "پیغام حذف شد" },
                StatusCode = 201,
                Success = true,
            };
            _logger.Error("SepehrAcademyApi : /DeleteMessage success:true");
            return sr;
        }
    }
}
