using EcomApplication.Entity;
using EcomApplication.Exceptions;
using EcomApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service
{
    internal class OrderProcessorService
    {
        private readonly OrderProcessorRepositoryImpl orderProcessor;
        public OrderProcessorService()
        {
            orderProcessor = new OrderProcessorRepositoryImpl();
        }
        public void AddProductService(Products product)
        {
            try
            {
                orderProcessor.CreateProduct(product);
                Console.WriteLine("Product Added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddCustomerService(Customer customer)
        {
            try
            {
                orderProcessor.CreateCustomer(customer);
                Console.WriteLine("Customer Added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteProductService(int productId)
        {
            try
            {
                ProductNotFoundException.ProductNotFound(productId);
                orderProcessor.DeleteProduct(productId);
                Console.WriteLine("Product deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteCustomerService(int customerId)
        {
            try
            {
                CustomerNotFoundException.CustomerNotFound(customerId);
                orderProcessor.DeleteProduct(customerId);
                Console.WriteLine("Customer deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddOrderService(Customer customer, Products product, int quantity)
        {


            try
            {
                CustomerNotFoundException.CustomerNotFound(customer.CustomerId);
                ProductNotFoundException.ProductNotFound(product.ProductId);
                orderProcessor.AddToCart(customer, product, quantity);
                Console.WriteLine("Product added to cart ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void DeleteFromCartService(Customer customer, Products product)
        {
            {
                try
                {
                    CustomerNotFoundException.CustomerNotFound(customer.CustomerId);
                    ProductNotFoundException.ProductNotFound(product.ProductId);
                    orderProcessor.RemoveFromCart(customer, product);
                    Console.WriteLine("Order deleted from cart ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void DisplayCartService(Customer customer)
        {
            try
            {
                CustomerNotFoundException.CustomerNotFound(customer.CustomerId);
                List<Cart> cartList = orderProcessor.GetAllFromCart(customer);
                Console.WriteLine("productid\tproductid\tstockquantity");
                foreach (Cart cart in cartList)
                {
                    Console.WriteLine(cart.CartId + "\t" + cart.CustomerId + "\t\t" + cart.ProductId + "\t\t" + cart.Quantity);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void PlaceOrder(Customer customer, List<Dictionary<Products, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                CustomerNotFoundException.CustomerNotFound(customer.CustomerId);
                orderProcessor.PlaceOrder(customer, productsAndQuantities, shippingAddress);
                Console.WriteLine("Order Placed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ViewOrder(int customerId)
        {
            try
            {
                CustomerNotFoundException.CustomerNotFound(customerId);
                List<Order_items> pairs = orderProcessor.GetOrdersByCustomer(customerId);
                Console.WriteLine("oredrItemid\tproductid\tquantity");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
