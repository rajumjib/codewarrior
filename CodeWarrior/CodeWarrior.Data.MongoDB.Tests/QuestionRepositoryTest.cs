using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using CodeWarrior.Data.MongoDB.Repositories;
using CodeWarrior.Business.Model;

namespace CodeWarrior.Data.MongoDB.Tests
{
    [TestClass]
    public class QuestionRepositoryTest
    {
        QuestionRepository repository = new QuestionRepository();

        [TestMethod]
        [Fact]
        public void GetAllTest()
        {
            
            var data = repository.GetAll();

            //Xunit.Assert.NotNull(data);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(data);

            //Xunit.Assert.True(data.Count() > 0);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(data.Count() > 0);

        }

        [TestMethod]
        [Fact]
        public void GetTest()
        {

            var data = repository.Get("538359db10fb8e1248981698");

            //Xunit.Assert.NotNull(data);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(data);

            //Xunit.Assert.True(data.Count() > 0);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(data.Body.Equals("Jahirul Islam"));

        }

        [TestMethod]
        [Fact]
        public void SaveTest()
        {
            var question = new Question
            {
                Body = "Jahirul Islam"
            };

            var data = repository.Save(question);

            //Xunit.Assert.NotNull(data);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(data);
        }

    }
}
