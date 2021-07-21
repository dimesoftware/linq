using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class PipeTests
    {
        [TestMethod]
        public void Pipe()
        {
            List<Customer> customers = new()
            {
                new Customer(1, "Jake Marquez", "New York"),
                new Customer(2, "Michael Jennings", "Pittsburgh"),
                new Customer(3, "Frank Hansom", "Phoenix"),
                new Customer(4, "Margareth Boyer", "New York")
            };

            string allCustomers = customers.Select(x => x.Id).Pipe(x => string.Join(",", x));
            Assert.IsTrue(allCustomers == "1,2,3,4");
        }
    }
}