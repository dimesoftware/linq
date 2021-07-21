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
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            };

            List<Customer> customers2 = new()
            {
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", "")
            };

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 3);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Join_SameType_ListOneHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", ""),
                new Customer(6, "Client 6", "")
            };

            List<Customer> customers2 = new()
            {
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", "")
            };

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 4);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Join_SameType_ListTwoHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            };

            List<Customer> customers2 = new()
            {
                new Customer(3, "Client 3", ""),
                new Customer(4, "Client 4", ""),
                new Customer(5, "Client 5", ""),
                new Customer(6, "Client 6", "")
            };

            IEnumerable<Customer> mergedLists = customers1.Merge<Customer>(customers2, (x, y) => new Customer(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 4);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Join_DifferentType_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            };

            List<Client> customers2 = new()
            {
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
            };

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y.Id, x.Name, x.Address));
            Assert.IsTrue(mergedLists.Count() == 3);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Join_DifferentType_FirstListHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", ""),
                new Customer(6, "Client 6", "")
            };

            List<Client> customers2 = new()
            {
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
            };

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y?.Id ?? x.Id, x?.Name ?? y.Name, x?.Address ?? y.Address));
            Assert.IsTrue(mergedLists.Count() == 4);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void Join_DifferentType_SecondListHasMoreItems_ReturnsCollectionTwoWithDataFromCollectionOne()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            };

            List<Client> customers2 = new()
            {
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", ""),
                new Client(6, "Client 6", "")
            };

            IEnumerable<Client> mergedLists = customers1.Merge(customers2, (x, y) => new Client(y?.Id ?? x.Id, x?.Name ?? y.Name, x?.Address ?? y.Address));
            Assert.IsTrue(mergedLists.Count() == 4);
            Assert.IsTrue(mergedLists.ElementAt(0).Name == "Customer 1");
        }

        [TestMethod]
        public void FullOuterJoin()
        {
            List<Customer> customers1 = new()
            {
                new Customer(1, "Customer 1", ""),
                new Customer(2, "Customer 2", ""),
                new Customer(3, "Customer 3", "")
            };

            List<Client> customers2 = new()
            {
                new Client(3, "Client 3", ""),
                new Client(4, "Client 4", ""),
                new Client(5, "Client 5", "")
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

            Assert.IsTrue(groups.Count() == 2);
            Assert.IsTrue(groups.ElementAt(0) == "Jose Guerrero Delgado");
            Assert.IsTrue(groups.ElementAt(1) == "Karen Smith");
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

            Assert.IsTrue(groups.Count() == 3);
            Assert.IsTrue(groups.ElementAt(0) == "Jose Guerrero");
            Assert.IsTrue(groups.ElementAt(1) == "Jose Delgado");
            Assert.IsTrue(groups.ElementAt(2) == "Karen Smith");
        }
    }
}