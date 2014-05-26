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
    public class TagRepository : ITagRepository
    {
        readonly MongoDatabase mongoDatabase;

        public TagRepository()
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


        public IEnumerable<Tag> GetAll()
        {
            List<Tag> model = new List<Tag>();
            var tagsList = mongoDatabase.GetCollection("Tags").FindAll().AsEnumerable();
            model = (from tag in tagsList
                     select new Tag
                     {
                         _Id = tag["_id"].AsString,
                         TagName = tag["TagName"].AsString
                     }).ToList();
            return model;
        }

        public Tag Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            var tagsList = mongoDatabase.GetCollection("Tags").Find(query).AsEnumerable();
            var model = (from tag in tagsList
                     select new Tag
                     {
                         _Id = tag["_id"].AsString,
                         TagName = tag["TagName"].AsString
                     }).FirstOrDefault<Tag>();
            return model;
        }

        public Tag Save(Tag tag)
        {
            var tagsList = mongoDatabase.GetCollection("Tags");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(tag._Id))
            {
                tag._Id = ObjectId.GenerateNewId().ToString();
                result = tagsList.Insert<Tag>(tag);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", tag._Id);
                IMongoUpdate update = Update
                    .Set("TagName", tag.TagName);
                result = tagsList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return tag;
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}