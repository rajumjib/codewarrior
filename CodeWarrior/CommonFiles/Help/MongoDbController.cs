//http://www.dotnetcurry.com/showarticle.aspx?ID=897

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDbWebAPi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MongoDbWebAPi.Controllers
{
    public class MongoDbController : ApiController
    {
        readonly MongoDatabase mongoDatabase;

        public MongoDbController()
        {
            mongoDatabase = RetreiveMongohqDb();
        }

        private MongoDatabase RetreiveMongohqDb()
        {
            MongoClient mongoClient = new MongoClient(
                new MongoUrl(ConfigurationManager.ConnectionStrings["MongoHQ"].ConnectionString));
            MongoServer server = mongoClient.GetServer();
            return mongoClient.GetServer().GetDatabase("MyFirstDb");
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            List<Contact> model = new List<Contact>();
            var contactsList = mongoDatabase.GetCollection("Contacts").FindAll().AsEnumerable();
            model = (from contact in contactsList
                     select new Contact
                     {
                         Id = contact["_id"].AsString,
                         Name = contact["Name"].AsString,
                         Address = contact["Address"].AsString,
                         Phone = contact["Phone"].AsString,
                         Email = contact["Email"].AsString
                     }).ToList();
            return model;
        }

        public Contact Save(Contact contact)
        {
            var contactsList = mongoDatabase.GetCollection("Contacts");
            WriteConcernResult result;
            bool hasError = false;
            if (string.IsNullOrEmpty(contact.Id))
            {
                contact.Id = ObjectId.GenerateNewId().ToString();
                result = contactsList.Insert<Contact>(contact);
                hasError = result.HasLastErrorMessage;
            }
            else
            {
                IMongoQuery query = Query.EQ("_id", contact.Id);
                IMongoUpdate update = Update
                    .Set("Name", contact.Name)
                    .Set("Address", contact.Address)
                    .Set("Phone", contact.Phone)
                    .Set("Email", contact.Email);
                result = contactsList.Update(query, update);
                hasError = result.HasLastErrorMessage;
            }
            if (!hasError)
            {
                return contact;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}

//Contact.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDbWebAPi.Models
{
    public class Contact
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

//Web.config
/*
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <connectionStrings>
    <add name="MongoHQ" connectionString="mongodb://administrator:password123@widmore.mongohq.com:10010/MyFirstDb" />

  </connectionStrings>
</configuration>
*/