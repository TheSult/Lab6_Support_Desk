using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportDeskLab
{
    public class Customer
    {
        public string CustomerId { get; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Customer(string id, string name, string email)
        {
            CustomerId = id;
            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return CustomerId + " | " + Name + " | " + Email;
        }
    }
}
