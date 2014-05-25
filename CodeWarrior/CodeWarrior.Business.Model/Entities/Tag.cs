using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Business.Model
{
    public partial class Tag
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string TagName
        {
            get;
            set;
        }

        public virtual ICollection<QuestionTag> QuestionTags
        {
            get
            {
                if (_questionTags == null)
                {
                    _questionTags = new List<QuestionTag>();
                }
                return _questionTags;
            }
            set
            {
                _questionTags = value;
            }
        }
        private ICollection<QuestionTag> _questionTags;
    }
}
