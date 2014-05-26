using CodeWarrior.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.Core.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();

        Tag Get(String id);

        Tag Save(Tag tag);
    }
}
