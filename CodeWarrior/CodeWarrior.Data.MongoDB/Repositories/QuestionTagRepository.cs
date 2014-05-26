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
    public class QuestionTagRepository : IQuestionTagRepository
    {
        readonly MongoDatabase mongoDatabase;

        public QuestionTagRepository()
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


        public IEnumerable<QuestionTag> GetAll()
        {
            List<QuestionTag> model = new List<QuestionTag>();
            var questionTagsList = mongoDatabase.GetCollection("QuestionTags").FindAll().AsEnumerable();
            model = (from questionTag in questionTagsList
                     select new QuestionTag
                     {
                         _Id = questionTag["_id"].AsString
                     }).ToList();
            return model;
        }

        public QuestionTag Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            var questionTagsList = mongoDatabase.GetCollection("QuestionTags").Find(query).AsEnumerable();
            var model = (from questionTag in questionTagsList
                     select new QuestionTag
                     {
                         _Id = questionTag["_id"].AsString
                     }).FirstOrDefault<QuestionTag>();
            return model;
        }

        public QuestionTag Save(QuestionTag questionTag)
        {
            var questionTagsList = mongoDatabase.GetCollection("QuestionTags");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(questionTag._Id))
            {
                questionTag._Id = ObjectId.GenerateNewId().ToString();
                result = questionTagsList.Insert<QuestionTag>(questionTag);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", questionTag._Id);
                IMongoUpdate update = Update
                    .Set("Tag", questionTag.Tag.TagName);
                result = questionTagsList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return questionTag;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}