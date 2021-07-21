using System;
using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class SelectExtensionsTests
    {
        [TestMethod]
        public void SelectTry_ListIsNull_ThrowsNullReferenceException()
        {
            static bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = null;
            Assert.ThrowsException<NullReferenceException>(() => customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId).ToList());
        }

        [TestMethod]
        public void SelectTry_ListIsPopulated_ReturnsIdentifiers()
        {
            static bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = new()
            {
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            IEnumerable<int> identifiers = customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId);
            Assert.IsTrue(identifiers.Sum() == 21);
        }

        [TestMethod]
        public void ConvertTo_IntToLong_ShouldReturnNewList()
        {
            IEnumerable<int> integerList = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<long> longList = integerList.ConvertTo<int, long>();
            Assert.IsTrue(longList.Count() == 5);
        }
    }
}