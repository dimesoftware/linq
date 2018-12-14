using System;
using System.Collections.Generic;
using System.Text;
using Dime.Linq.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dime.Linq.Tests
{
    [TestClass]
    public class LinqUtilitiesTests
    {
        [TestMethod]
        public void Linq_IfNotNull_ItemIsNull_ReturnsDefaultValue()
        {
            Customer customer = null;
            string result = customer.IfNotNull<Customer, string>(x => "Hello world!", "Goodbye world!");

            Assert.IsTrue(result == "Goodbye world!");
        }

        [TestMethod]
        public void Linq_IfNotNull_ItemIsNotNull_ReturnsDefaultValue()
        {
            Customer customer = new Customer();
            string result = customer.IfNotNull<Customer, string>(x => "Hello world!", "Goodbye world!");

            Assert.IsTrue(result == "Hello world!");
        }

        [TestMethod]
        public void Linq_SelectTry_ListIsNull_ThrowsNullReferenceException()
        {
            bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = null;
            Assert.ThrowsException<NullReferenceException>(() => customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId).ToList());
        }

        [TestMethod]
        public void Linq_SelectTry_ListIsPopulated_ReturnsIdentifiers()
        {
            bool ParseId(Client client, out int number)
            {
                number = client.Id;
                return true;
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            IEnumerable<int> identifiers = customers.SelectTry<Customer, Client, int>(x => new Client() { Id = x.Id }, ParseId);
            Assert.IsTrue(identifiers.Sum() == 21);
        }

        [TestMethod]
        public void Linq_First_SourceIsNull_ThrowsException()
        {
            List<Customer> customers = null;
            Assert.ThrowsException<ArgumentNullException>(() => customers.First(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful"));
        }

        [TestMethod]
        public void Linq_First_FirstFilterPasses_ReturnsItem()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.First(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void Linq_First_SecondFilterPasses_ReturnsItem()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.First(x => x.Id > 1, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 5);
        }

        [TestMethod]
        public void Linq_First_NoFilterPasses_ReturnsDefault()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.First(x => x.Id > 15, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void Linq_FirstOrDefault_SourceIsNull_ThrowsException()
        {
            List<Customer> customers = null;

            Assert.ThrowsException<ArgumentNullException>(() => customers.FirstOrDefault(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful"));
        }

        [TestMethod]
        public void Linq_FirstOrDefault_FirstFilterPasses_ReturnsItem()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Hello", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 0, x => x.Name != "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void Linq_FirstOrDefault_SecondFilterPasses_ReturnsItem()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 1, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 5);
        }

        [TestMethod]
        public void Linq_FirstOrDefault_NoFilterPasses_ReturnsDefault()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Handsome B. Wonderful", "World"),
                new Customer(5, "Hello", "World"),
                new Customer(15, "Hello", "World")
            };

            Customer firstCustomer = customers.FirstOrDefault(x => x.Id > 15, x => x.Name == "Handsome B. Wonderful");
            Assert.IsTrue(firstCustomer.Id == 1);
        }

        [TestMethod]
        public void Linq_CatchExceptions_LoopsWithoutErrors()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Handsome B. Wonderful", "World"),
                null,
                new Customer(15, "Hello", "World")
            };

            List<Client> clients = customers.Select(x => new Client { Id = x.Id }).CatchExceptions(x => { }).ToList();
            Assert.IsTrue(clients.Count() == 2);
        }


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
