using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Business.Model
{
    public partial class QuestionTag
    {
        public virtual int Id { get; set; }

        [BsonId]
        public virtual string _Id { get; set; }

        public virtual int TagId
        {
            get { return _tagId; }
            set
            {
                if (_tagId != value)
                {
                    if (Tag != null && Tag.Id != value)
                    {
                        Tag = null;
                    }
                    _tagId = value;
                }
            }
        }
        private int _tagId;

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

        public virtual Question Question { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
