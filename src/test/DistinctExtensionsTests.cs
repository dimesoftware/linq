using System;
using System.Collections.Generic;
using System.Linq;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class DistinctExtensionsTests
    {
        [TestMethod]
        public void Linq_DistinctBy_Tuples_ShouldReturnDistinctItems()
        {
            List<Tuple<int, Customer>> customers = new List<Tuple<int, Customer>>()
            {
                new Tuple<int, Customer>(1, new Customer(1, "Category 1", "Address 1")),
                new Tuple<int, Customer>(2, new Customer(2, "Category 2", "Address 1")),
                new Tuple<int, Customer>(3, new Customer(3, "Category 1", "Address 1")),
                new Tuple<int, Customer>(4, new Customer(4, "Category 3", "Address 1")),
                new Tuple<int, Customer>(5, new Customer(5, "Category 1", "Address 1")),
                new Tuple<int, Customer>(6, new Customer(6, "Category 2", "Address 1"))
            };

            Assert.IsTrue(customers.DistinctBy(x => x.Name).Count() == 3);
        }

        [TestMethod]
        public void Linq_DistinctBy_List_ShouldReturnDistinctItems()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(1, "Category 1", "Address 1"),
                new Customer(2, "Category 2", "Address 1"),
                new Customer(3, "Category 1", "Address 1"),
                new Customer(4, "Category 3", "Address 1"),
                new Customer(5, "Category 1", "Address 1"),
                new Customer(6, "Category 2", "Address 1")
            };

            Assert.IsTrue(customers.DistinctBy(x => x.Name).Count() == 3);
        }

        [TestMethod]
        public void Linq_DistinctBy_List_Filter_ShouldReturnDistinctItems()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(1, "Category 1", "Address 1"),
                new Customer(2, "Category 2", "Address 1"),
                new Customer(3, "Category 1", "Address 1"),
                new Customer(4, "Category 3", "Address 1"),
                new Customer(5, "Category 1", "Address 1"),
                new Customer(6, "Category 2", "Address 1")
            };

            Assert.IsTrue(customers.DistinctBy(x => x.Name, x => x.Id != 4).Count() == 2);
        }
    }
}