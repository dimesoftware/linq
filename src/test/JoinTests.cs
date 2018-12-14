using System;
using System.Collections.Generic;
using System.Text;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class JoinTests
    {
        [TestMethod]
        public void Linq_Join_SameType_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new List<Customer>
            {
                new Customer(1, "Customer 1",""),
                new Customer(2,"Customer 2",""),
                new Customer(3,"Customer 3","")
            };

            List<Customer> customers2 = new List<Customer>
            {
                new Customer(3, "Client 3",""),
                new Customer(4,"Client 4",""),
                new Customer(5,"Client 5","")
            };

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 3);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Linq_Join_DifferentType_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new List<Customer>
            {
                new Customer(1, "Customer 1",""),
                new Customer(2,"Customer 2",""),
                new Customer(3,"Customer 3","")
            };

            List<Client> customers2 = new List<Client>
            {
                new Client(3, "Client 3",""),
                new Client(4,"Client 4",""),
                new Client(5,"Client 5","")
            };

            IEnumerable<Client> mergedLists = customers1.Merge<Customer, Client, Client>(customers2, (x, y) => new Client(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 3);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Linq_FullOuterJoin()
        {
            List<Customer> customers1 = new List<Customer>
            {
                new Customer(1,"Customer 1",""),
                new Customer(2,"Customer 2",""),
                new Customer(3,"Customer 3","")
            };

            List<Client> customers2 = new List<Client>
            {
                new Client(3, "Client 3",""),
                new Client(4,"Client 4",""),
                new Client(5,"Client 5","")
            };

            IEnumerable<Client> mergedLists = customers1.FullOuterJoin(
                customers2,
                x => x.Id,
                y => y.Id,
                (x, y, z) => new Client(x.Id, x.Name, x.Address),
                new Customer(0, "Unknown customer", ""),
                new Client(0, "Unknown client", ""))
                .ToList();

            Assert.IsTrue(mergedLists.Count() == 5);
            Assert.IsTrue(mergedLists.ElementAt(2).Name == "Customer 3");
        }
    }
}
