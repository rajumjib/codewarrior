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
    public class AnswerRepositoryTest
    {
        AnswerRepository repository = new AnswerRepository();

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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(data.AnswerText.Equals("Jahirul Islam"));

        }

        [TestMethod]
        [Fact]
        public void SaveTest()
        {
            var answer = new Answer
            {
                AnswerText = "Jahirul Islam"
            };

            var data = repository.Save(answer);

            //Xunit.Assert.NotNull(data);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(data);
        }

    }
}
