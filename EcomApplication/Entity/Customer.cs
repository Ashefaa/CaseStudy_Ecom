using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Entity
{
    public class Customer
    {

        public int customerId;
        public string name;
        public string email;
        public string password;

        //default constructor
        public Customer()
        { }
        //parameterized constructor
        public Customer(int customerId, string name, string email, string password)
        {
            this.customerId = customerId;
            this.name = name;
            this.email = email;
            this.password = password;
        }
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public override string ToString()
        {
            return $"{CustomerId} {Name} {Email} {Password}";
        }
    }
}
