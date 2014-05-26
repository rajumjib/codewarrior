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
    public class QuestionRepository : IQuestionRepository
    {
        readonly MongoDatabase mongoDatabase;

        public QuestionRepository()
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


        public IEnumerable<Question> GetAll()
        {
            List<Question> model = new List<Question>();
            var questionsList = mongoDatabase.GetCollection("Questions").FindAll().AsEnumerable();
            model = (from question in questionsList
                     select new Question
                     {
                         _Id = question["_id"].AsString,
                         Body = question["Body"].AsString
                     }).ToList();
            return model;
        }

        public Question Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            var questionsList = mongoDatabase.GetCollection("Questions").Find(query).AsEnumerable();
            var model = (from question in questionsList
                     select new Question
                     {
                         _Id = question["_id"].AsString,
                         Body = question["Body"].AsString
                     }).FirstOrDefault<Question>();
            return model;
        }

        public Question Save(Question question)
        {
            var questionsList = mongoDatabase.GetCollection("Questions");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(question._Id))
            {
                question._Id = ObjectId.GenerateNewId().ToString();
                result = questionsList.Insert<Question>(question);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", question._Id);
                IMongoUpdate update = Update
                    .Set("Body", question.Body)
                    .Set("Votes", question.Votes);
                result = questionsList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return question;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}