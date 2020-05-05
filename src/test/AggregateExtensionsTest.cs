using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class AggregateExtensionsTest
    {
        [TestMethod]
        public void AggregateExtensions_List_JoinString_ShouldReturnConcatenatedString()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(1, "Jake Marquez", "New York"),
                new Customer(2, "Michael Jennings", "Pittsburgh"),
                new Customer(3, "Frank Hansom", "Phoenix"),
                new Customer(4, "Margareth Boyer", "New York")
            };

            string allCustomers = customers.Aggregate(x => string.Join(",", x.Select(y => y.Id)));
            Assert.IsTrue(allCustomers == "1,2,3,4");
        }
    }
}