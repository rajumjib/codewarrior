using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Business.Model
{
    public partial class Question
    {
        
        public virtual int Id { get; set; }

        [BsonId]
        public virtual string _Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Body { get; set; }

        public virtual int Votes { get; set; }

        public virtual int Views { get; set; }

        public virtual string Slug { get; set; }

        public virtual byte[] ConcurrencyStamp { get; set; }

        public virtual DateTime DateCreated { get; set; }

        public virtual Nullable<DateTime> DateExpires { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                if (_answers == null)
                {
                    _answers = new List<Answer>();
                }
                return _answers;
            }
            set
            {
                _answers = value;
            }
        }
        private ICollection<Answer> _answers;

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
