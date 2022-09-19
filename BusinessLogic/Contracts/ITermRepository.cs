using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BusinessLogic.Contracts
{
    public interface ITermRepository
    {

        Task<List<TermModel>> GetAllTerms();
        Task<bool> CreateTerm(TermModel term);

        Task<TermModel> GetTermById(int termId);

        public void Save();
    }
}
