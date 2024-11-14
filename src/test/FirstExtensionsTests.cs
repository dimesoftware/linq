using System;
using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class FirstExtensionsTests
    {
        [TestMethod]
        public void First_SourceIsNull_ThrowsException()
        {
            List<Customer> customers = null;
            Assert.ThrowsException<ArgumentNullException>(() => customers.First(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful"));
        }

        [TestMethod]
        public void First_FirstFilterPasses_ReturnsItem()
        {
            List<Customer> customers =
            [
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.First(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void First_SecondFilterPasses_ReturnsItem()
        {
            List<Customer> customers =
            [
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.First(x => x.Id > 1, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 5);
        }

        [TestMethod]
        public void First_NoFilterPasses_ReturnsDefault()
        {
            List<Customer> customers =
            [
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.First(x => x.Id > 15, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void FirstOrDefault_SourceIsNull_ThrowsException()
        {
            List<Customer> customers = null;

            Assert.ThrowsException<ArgumentNullException>(() => customers.FirstOrDefault(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful"));
        }

        [TestMethod]
        public void FirstOrDefault_FirstFilterPasses_ReturnsItem()
        {
            List<Customer> customers =
            [
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void FirstOrDefault_SecondFilterPasses_ReturnsItem()
        {
            List<Customer> customers =
            [
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 1, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 5);
        }

        [TestMethod]
        public void FirstOrDefault_NoFilterPasses_ReturnsDefault()
        {
            List<Customer> customers =
            [
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            ];

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 15, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }
    }
}