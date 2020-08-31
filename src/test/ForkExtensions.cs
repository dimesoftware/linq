using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class ForkExtensions
    {
        [TestMethod]
        public void Linq_Fork_WithDataInBothSets_ShouldSplitIntoTwo_PopulatedSets()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Amanda Hugankiss", "Moe's Tavern"),
                new Customer(2, "Hugh Jazz", "Bumtown"),
                new Customer(3, "I.P. Freely", "Bumtown")
            };

            (IEnumerable<Customer> success, IEnumerable<Customer> failed) = customers.Fork(x => x.Address == "Bumtown");

            Assert.IsTrue(success.Count() == 2);
            Assert.IsTrue(failed.Count() == 1);
        }

        [TestMethod]
        public void Linq_Fork_WithEmptyDataInOneSet_ShouldSplitIntoTwo_EmptyCollection()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(2, "Hugh Jazz", "Bumtown"),
                new Customer(3, "I.P. Freely", "Bumtown")
            };

            (IEnumerable<Customer> success, IEnumerable<Customer> failed) = customers.Fork(x => x.Address == "Bumtown");

            Assert.IsTrue(success.Count() == 2);
            Assert.IsTrue(!failed.Any());
        }

        [TestMethod]
        public void Linq_Fork_WithEmptyDataInBothSet_ShouldSplitIntoTwo_NoData()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Amanda Hugankiss", "Moe's Tavern"),
                new Customer(2, "Hugh Jazz", "Bumtown"),
                new Customer(3, "I.P. Freely", "Bumtown")
            };

            (IEnumerable<Customer> success, IEnumerable<Customer> failed) = customers.Fork(x => x.Address == "Not Bumtown");

            Assert.IsTrue(!success.Any());
            Assert.IsTrue(failed.Count() == 3);
        }
    }
}