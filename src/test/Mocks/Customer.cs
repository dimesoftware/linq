namespace Dime.Linq.Tests.Mocks
{
    internal class Customer
    {
        public Customer()
        {
        }

        public Customer(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    internal class Client
    {
        public Client()
        {
        }

        public Client(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}