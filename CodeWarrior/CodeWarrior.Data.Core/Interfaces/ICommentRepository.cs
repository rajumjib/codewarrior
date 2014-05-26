using CodeWarrior.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.Core.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();

        Comment Get(String id);

        Comment Save(Comment comment);
    }
}
