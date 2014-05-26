using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Business.Model
{
    public partial class Comment
    {
        public virtual int Id { get; set; }

        [BsonId]
        public virtual string _Id { get; set; }

        public virtual string Body { get; set; }

        public virtual int AnswerId
        {
            get { return _answerId; }
            set
            {
                if (_answerId != value)
                {
                    if (Answer != null && Answer.Id != value)
                    {
                        Answer = null;
                    }
                    _answerId = value;
                }
            }
        }
        private int _answerId;

        public virtual Answer Answer { get; set; }
    }
}
