using EcomApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Exceptions
{
    internal class OrderNotFoundException:Exception
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public static void OrderNotFound(int orderid)
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (!orderProcessorRepositoryImpl.OrderNotExist(orderid))
                throw new CustomerNotFoundException("Customer not found!!!");

        }
    }
}
