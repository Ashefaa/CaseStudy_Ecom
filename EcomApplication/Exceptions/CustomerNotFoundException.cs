using EcomApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Exceptions
{
    internal class CustomerNotFoundException:Exception
    {
        public CustomerNotFoundException(string message) : base(message)
        {
        }
        public static void CustomerNotFound(int customerid)
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (!orderProcessorRepositoryImpl.CustomerNotPresent((customerid)))
                throw new CustomerNotFoundException("Customer not found!!!");

        }
    }
}
