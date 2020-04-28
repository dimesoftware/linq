using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class AnyExtensionsTests
    {
        [TestMethod]
        public void Linq_IsNullOrEmpty_IsNull_ReturnsTrue()
        {
            List<Customer> customers = null;
            Assert.IsTrue(customers.IsNullOrEmpty());
        }

        [TestMethod]
        public void Linq_IsNullOrEmpty_IsEmpty_ReturnsTrue()
        {
            List<Customer> customers = new List<Customer>();
            Assert.IsTrue(customers.IsNullOrEmpty());
        }

        [TestMethod]
        public void Linq_IsNullOrEmpty_IsEmpty_ReturnsFalse()
        {
            List<Customer> customers = new List<Customer>() { new Customer() };
            Assert.IsFalse(customers.IsNullOrEmpty());
        }
    }
}
