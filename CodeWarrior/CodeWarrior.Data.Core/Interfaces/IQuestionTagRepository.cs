using CodeWarrior.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.Core.Interfaces
{
    public interface IQuestionTagRepository
    {
        IEnumerable<QuestionTag> GetAll();

        QuestionTag Get(String id);

        QuestionTag Save(QuestionTag questionTag);
    }
}
