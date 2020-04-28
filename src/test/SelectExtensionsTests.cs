using System;
using System.Collections.Generic;
using System.Text;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class SelectExtensionsTests
    {
        [TestMethod]
        public void Linq_SelectTry_ListIsNull_ThrowsNullReferenceException()
        {
            bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = null;
            Assert.ThrowsException<NullReferenceException>(() => customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId).ToList());
        }

        [TestMethod]
        public void Linq_SelectTry_ListIsPopulated_ReturnsIdentifiers()
        {
            bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            IEnumerable<int> identifiers = customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId);
            Assert.IsTrue(identifiers.Sum() == 21);
        }
    }
}
