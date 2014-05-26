using CodeWarrior.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.Core.Interfaces
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAll();

        Answer Get(String id);

        Answer Save(Answer answer);
    }
}
