using CodeWarrior.Business.Model;
using CodeWarrior.Data.Core.Interfaces;
using CodeWarrior.Data.MongoDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeWarrior.Service.Controllers
{
    public class QuestionsController : ApiController
    {

        IQuestionRepository questionRepository;

        public QuestionsController() {
            questionRepository = new QuestionRepository();
        }

        public QuestionsController(IQuestionRepository repository)
        {
            questionRepository = repository;
        }

        // GET api/questions
        public IEnumerable<Question> Get()
        {
            return questionRepository.GetAll();
        }

        // GET api/questions/5
        public Question Get(string id)
        {
            return questionRepository.Get(id);
        }

        // POST api/questions
        public void Post([FromBody]Question value)
        {
            questionRepository.Save(value);
        }

        // PUT api/questions/5
        public void Put(string id, [FromBody]Question value)
        {
        }

        // DELETE api/questions/5
        public void Delete(string id)
        {
        }
    }
}