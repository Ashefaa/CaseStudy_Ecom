using EcomApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Exceptions
{
    internal class ProductNotFoundException:Exception
    {

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public static void ProductNotFound(int productid)
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (!orderProcessorRepositoryImpl.ProductNotFound(productid))
                throw new ProductNotFoundException("Product not found!!!");

        }
    }
}
