using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Business.Model
{

    public partial class Answer
    {
        public virtual int Id { get; set; }

        [BsonId]
        public virtual string _Id { get; set; }

        public virtual int QuestionId
        {
            get { return _questionId; }
            set
            {
                if (_questionId != value)
                {
                    if (Question != null && Question.Id != value)
                    {
                        Question = null;
                    }
                    _questionId = value;
                }
            }
        }
        private int _questionId;

        public virtual string AnswerText { get; set; }

        public virtual int Votes { get; set; }

        public virtual System.DateTime DateCreated { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                if (_comments == null)
                {
                    _comments = new List<Comment>();
                }
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }
        private ICollection<Comment> _comments;
    }
}
