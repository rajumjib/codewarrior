using CodeWarrior.Business.Model;
using CodeWarrior.Data.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWarrior.Data.MongoDB.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        readonly MongoDatabase mongoDatabase;

        public CommentRepository()
        {
            mongoDatabase = RetreiveMongohqDb();
        }

        private MongoDatabase RetreiveMongohqDb()
        {
            MongoClient mongoClient = new MongoClient(
                new MongoUrl(ConfigurationManager.ConnectionStrings["MongoHQ"].ConnectionString));
            MongoServer server = mongoClient.GetServer();
            return mongoClient.GetServer().GetDatabase("codewarrior");
        }


        public IEnumerable<Comment> GetAll()
        {
            List<Comment> model = new List<Comment>();
            var commentsList = mongoDatabase.GetCollection("Comments").FindAll().AsEnumerable();
            model = (from comment in commentsList
                     select new Comment
                     {
                         _Id = comment["_id"].AsString,
                         Body = comment["Body"].AsString
                     }).ToList();
            return model;
        }

        public Comment Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            var commentsList = mongoDatabase.GetCollection("Comments").Find(query).AsEnumerable();
            var model = (from comment in commentsList
                     select new Comment
                     {
                         _Id = comment["_id"].AsString,
                         Body = comment["Body"].AsString
                     }).FirstOrDefault<Comment>();
            return model;
        }

        public Comment Save(Comment comment)
        {
            var commentsList = mongoDatabase.GetCollection("Comments");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(comment._Id))
            {
                comment._Id = ObjectId.GenerateNewId().ToString();
                result = commentsList.Insert<Comment>(comment);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", comment._Id);
                IMongoUpdate update = Update
                    .Set("Body", comment.Body);
                result = commentsList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return comment;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}