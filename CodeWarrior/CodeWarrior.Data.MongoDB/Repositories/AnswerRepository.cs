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
    public class AnswerRepository : IAnswerRepository
    {
        readonly MongoDatabase mongoDatabase;

        public AnswerRepository()
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


        public IEnumerable<Answer> GetAll()
        {
            List<Answer> model = new List<Answer>();
            var answersList = mongoDatabase.GetCollection("Answers").FindAll().AsEnumerable();
            model = (from answer in answersList
                     select new Answer
                     {
                         _Id = answer["_id"].AsString,
                         AnswerText = answer["AnswerText"].AsString
                     }).ToList();
            return model;
        }

        public Answer Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            var answersList = mongoDatabase.GetCollection("Answers").Find(query).AsEnumerable();
            var model = (from answer in answersList
                     select new Answer
                     {
                         _Id = answer["_id"].AsString,
                         AnswerText = answer["AnswerText"].AsString
                     }).FirstOrDefault<Answer>();
            return model;
        }

        public Answer Save(Answer answer)
        {
            var answersList = mongoDatabase.GetCollection("Answers");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(answer._Id))
            {
                answer._Id = ObjectId.GenerateNewId().ToString();
                result = answersList.Insert<Answer>(answer);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", answer._Id);
                IMongoUpdate update = Update
                    .Set("AnswerText", answer.AnswerText)
                    .Set("Votes", answer.Votes);
                result = answersList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return answer;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}