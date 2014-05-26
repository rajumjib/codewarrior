using CodeWarrior.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.Core.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAll();

        Question Get(String id);

        Question Save(Question question);
    }
}
