using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class AnyExtensionsTests
    {
        [TestMethod]
        public void IsNullOrEmpty_IsNull_ReturnsTrue()
        {
            List<Customer> customers = null;
            Assert.IsTrue(customers.IsNullOrEmpty());
        }

        [TestMethod]
        public void IsNullOrEmpty_IsEmpty_ReturnsTrue()
        {
            List<Customer> customers = new();
            Assert.IsTrue(customers.IsNullOrEmpty());
        }

        [TestMethod]
        public void IsNullOrEmpty_IsEmpty_ReturnsFalse()
        {
            List<Customer> customers = new() { new Customer() };
            Assert.IsFalse(customers.IsNullOrEmpty());
        }
    }
}