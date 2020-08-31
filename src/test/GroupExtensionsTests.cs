using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class GroupExtensionsTests
    {
        [TestMethod]
        public void GroupByMany_NoDuplicates_ShouldReturnFlatGroup()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Jeff", "New York"),
                new Customer(2, "Harvey", "Los Angeles"),
                new Customer(3, "Donald", "New York"),
                new Customer(4, "Megan", "Toronto"),
                new Customer(5, "Frank", "New York"),
            };

            List<IGrouping<object, Customer>> groups = customers.GroupByMany(x => x.Name, x => x.Address).ToList();
            Assert.IsTrue(groups.Count == 5);

            Assert.IsTrue(
                groups
                    .FirstOrDefault(x =>
                    {
                        object[] keys = x.Key as object[];
                        return (string)keys[0] == "Jeff" && (string)keys[1] == "New York";
                    })
                    .Count() == 1);
        }

        [TestMethod]
        public void GroupByMany_HasDuplicates_ShouldReturnNestedGroups()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Jeff", "New York"),
                new Customer(2, "Harvey", "Los Angeles"),
                new Customer(3, "Donald", "New York"),
                new Customer(4, "Megan", "Toronto"),
                new Customer(5, "Jeff", "New York"),
            };

            List<IGrouping<object, Customer>> groups = customers.GroupByMany(x => x.Name, x => x.Address).ToList();
            Assert.IsTrue(groups.Count == 4);

            Assert.IsTrue(
                groups
                    .FirstOrDefault(x =>
                    {
                        object[] keys = x.Key as object[];
                        return (string)keys[0] == "Jeff" && (string)keys[1] == "New York";
                    })
                    .Count() == 2);
        }
    }
}