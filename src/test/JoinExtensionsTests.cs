using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class JoinExtensionsTests
    {
        [TestMethod]
        public void Join_SameType_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            ];

            List<Customer> customers2 =
            [
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", "")
            ];

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.AreEqual(3, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void Join_SameType_ListOneHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", ""),
                new Customer(6, "Client 6", "")
            ];

            List<Customer> customers2 =
            [
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", "")
            ];

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.AreEqual(4, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void Join_SameType_ListTwoHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            ];

            List<Customer> customers2 =
            [
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", ""),
                new Customer(6, "Client 6", "")
            ];

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.AreEqual(4, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void Join_DifferentType_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            ];

            List<Client> customers2 =
            [
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
            ];

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y.Id, x.Name, x.Address));
            Assert.AreEqual(3, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void Join_DifferentType_FirstListHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", ""),
                new Customer(6, "Client 6", "")
            ];

            List<Client> customers2 =
            [
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
            ];

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y?.Id ?? x.Id, x?.Name ?? y.Name, x?.Address ?? y.Address));
            Assert.AreEqual(4, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void Join_DifferentType_SecondListHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            ];

            List<Client> customers2 =
            [
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", ""),
                new Client(6, "Client 6", "")
            ];

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y?.Id ?? x.Id, x?.Name ?? y.Name, x?.Address ?? y.Address));
            Assert.AreEqual(4, mergedLists.Count());
            Assert.AreEqual("Customer 1", mergedLists.ElementAt(0).Name);
        }

        [TestMethod]
        public void FullOuterJoin()
        {
            List<Customer> customers1 =
            [
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            ];

            List<Client> customers2 =
            [
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
            ];

            IEnumerable<Client> mergedLists = customers1.FullOuterJoin(
                customers2,
                x => x.Id,
                y => y.Id,
                (x, y, z) => new Client(x.Id, x.Name, x.Address),
                new Customer(0, "Unknown customer", ""),
                new Client(0, "Unknown client", ""))
                .ToList();

            Assert.AreEqual(5, mergedLists.Count());
            Assert.AreEqual("Customer 3", mergedLists.ElementAt(2).Name);
        }

        [TestMethod]
        public void FullOuterGroupJoin_Matches_ShouldJoin()
        {
            Customer[] ax = new Customer[]
            {
                new Customer { Id = 1, Name = "Jose" },
                new Customer { Id = 2, Name = "Karen" }
            };

            Customer[] bx = new Customer[]
            {
                new Customer { Id = 1, LastName = "Guerrero" },
                new Customer { Id = 1, LastName = "Delgado" },
                new Customer { Id = 2, LastName = "Smith" }
            };

            List<string> groups = ax.FullOuterGroupJoin(
                bx,
                a => a.Id,
                b => b.Id,
                (a, b, id) =>
                {
                    string lastNames = string.Join(" ", b.Select(x => x.LastName));
                    return string.Join(",", a.Select(x => x.Name + " " + lastNames));
                })
                .ToList();

            Assert.AreEqual(2, groups.Count);
            Assert.AreEqual("Jose Guerrero Delgado", groups.ElementAt(0));
            Assert.AreEqual("Karen Smith", groups.ElementAt(1));
        }

        [TestMethod]
        public void LeftOuterJoin_ShouldJoin()
        {
            Customer[] ax = new Customer[]
            {
                new Customer { Id = 1, Name = "Jose" },
                new Customer { Id = 2, Name = "Karen" }
            };

            Customer[] bx = new Customer[]
            {
                new Customer { Id = 1, LastName = "Guerrero" },
                new Customer { Id = 1, LastName = "Delgado" },
                new Customer { Id = 2, LastName = "Smith" }
            };

            List<string> groups = ax.LeftOuterJoin(
                bx,
                a => a.Id,
                b => b.Id,
                (a, b) => a.Name + " " + b.LastName)
                .ToList();

            Assert.AreEqual(3, groups.Count);
            Assert.AreEqual("Jose Guerrero", groups.ElementAt(0));
            Assert.AreEqual("Jose Delgado", groups.ElementAt(1));
            Assert.AreEqual("Karen Smith", groups.ElementAt(2));
        }
    }
}