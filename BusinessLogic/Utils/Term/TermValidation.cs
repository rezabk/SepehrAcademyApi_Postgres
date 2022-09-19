using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Contracts;
using DAL.DTO.Auth;
using DAL.DTO.Course;
using DAL.Model;
using Data;
using Serilog;

namespace BusinessLogic.Utils.Course
{
    public class TermValidation
    {
        private readonly ITermRepository _term;
        private readonly Serilog.ILogger _logger = Log.Logger;
        private readonly ITermStudentRepository _termStudent;

        public TermValidation(ITermRepository term, ITermStudentRepository termStudent)
        {
            _term = term;
            _termStudent = termStudent;
        }
        public async Task<StandardResult> ValidateDto(CreateTermDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "لطفا نام دوره را وارد کنید " },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /CreateTerm success:false");
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

   

        public async Task<StandardResult> CheckNullTerms(List<TermModel> terms)
        {
            if (terms.Count == 0)
            {
                var er = new StandardResult<string>
                {
                    Messages = new List<string> { "هیچ دوره ای وجود ندارد" },
                    Result = null,
                    StatusCode = 200,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetAllTerms success:false");
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


        public async Task<StandardResult> CheckNullTerm(TermModel term)
        {
            if (term is null)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "دوره ای با ایدی داده شده وجود ندارد" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /GetTermById success:false");
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

        public async Task<StandardResult> CheckStudentAlreadyJoinedTerm(int studentId, int termId)
        {
            var checkStudentAlreadyJoinedTerm =
                await _termStudent.CheckStudentAlreadyJoinedTerm(studentId, termId);

            if (checkStudentAlreadyJoinedTerm)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "دانشجو قبلا در این دوره ثبت نام کرده است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /AddStudentToTerm success:false");
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


        public async Task<StandardResult> CheckStudentJoinedTerm(int studentId, int termId)
        {
            var checkStudentAlreadyJoinedTerm =
                await _termStudent.CheckStudentAlreadyJoinedTerm(studentId, termId);

            if (checkStudentAlreadyJoinedTerm == false)
            {
                var er = new StandardResult
                {
                    Messages = new List<string> { "دانشجو در این دوره ثبت نام نکرده است" },
                    StatusCode = 404,
                    Success = false,
                };
                _logger.Error("SepehrAcademyApi : /AddStudentToTerm success:false");
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
