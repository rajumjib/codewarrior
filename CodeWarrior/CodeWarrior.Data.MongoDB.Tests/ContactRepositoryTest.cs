using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace CodeWarrior.Data.MongoDB.Tests
{
    [TestClass]
    public class ContactRepositoryTest
    {
        ContactRepository repository = new ContactRepository();

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
        public void SaveTest()
        {
            var contact = new Contact
            {
                Name = "Jahirul Islam",
                Address = "Dhaka",
                Email = "raju_mjib@yahoo.com",
                Phone = "+8801914292121"
            };

            var data = repository.Save(contact);

            //Xunit.Assert.NotNull(data);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(data);
        }

        [Fact]
        public void DivideByZeroThrowsException()
        {
            Xunit.Assert.Throws<System.DivideByZeroException>(
                delegate
                {
                    DivideNumbers(5, 0);
                });
        }

        public int DivideNumbers(int theTop, int theBottom)
        {
            return theTop / theBottom;
        }

        [Fact(Timeout = 50)]
        public void TestThatRunsTooLong()
        {
            System.Threading.Thread.Sleep(250);
        }

        [Fact(Skip = "Can't figure out where this is going wrong...")]
        public void BadMath()
        {
            Xunit.Assert.Equal(5, 2 + 2);
        }
    }
}
