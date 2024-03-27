using EcomApplication.Entity;
using EcomApplication.Repositories;
using EcomApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EcomApplication.EcomMenu
{
    internal class EcomApp
    {
        readonly OrderProcessorRepositoryImpl orderProcessor;
        public EcomApp()
        {
            orderProcessor = new OrderProcessorRepositoryImpl();
        }
        public void Run()
        {
            bool running = true;

            while (running)
            {
                OrderProcessorService orderProcessorService = new OrderProcessorService();
                Console.WriteLine("Ecommerce");
                Console.WriteLine("1.Add product in product table");
                Console.WriteLine("2.Add Customer in customer table");
                Console.WriteLine("3.Delete Product in product table");
                Console.WriteLine("4.Delete Customer in customer table");
                Console.WriteLine("5.Add product to cart");
                Console.WriteLine("6.Delete product from cart");
                Console.WriteLine("7.Display cart for a customer");
                Console.WriteLine("8.Place order");
                Console.WriteLine("9.View Customer Order");
                Console.WriteLine("Enter your Input");

                int choice = int.Parse(Console.ReadLine());
                Products product = new Products();
                Customer customer = new Customer();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Product Name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Product price: ");
                        int price = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Product description : ");
                        string description = Console.ReadLine();
                        Console.WriteLine("Enter stock quantity: ");
                        int stockquantity = int.Parse(Console.ReadLine());
                        product = new Products() { ProductName = name, Price = price, Description = description, StockQuantity = stockquantity };
                        orderProcessorService.AddProductService(product);
                        break;
                    case 2:
                        Console.WriteLine("Enter Name: ");
                        string customername = Console.ReadLine();
                        Console.WriteLine("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter password : ");
                        string password = Console.ReadLine();
                        customer = new Customer()
                        {
                            Name = customername,
                            Email = email,
                            Password = password
                        };
                        orderProcessorService.AddCustomerService(customer);
                        break;
                    case 3:
                        Console.WriteLine("Enter Product Id: ");
                        int deleteproductid = int.Parse(Console.ReadLine());
                        orderProcessorService.DeleteProductService(deleteproductid);
                        break;

                    case 4:
                        Console.WriteLine("Enter Customer Id: ");
                        int deletecustomerid = int.Parse(Console.ReadLine());
                        orderProcessorService.DeleteCustomerService(deletecustomerid);
                        break;
                    case 5:
                        Console.WriteLine("Enter the customer id:");
                        int customerid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the product id:");
                        int productid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the quantity:");
                        int quantity = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = customerid,
                        };
                        product = new Products()
                        {
                            ProductId = productid,
                        };
                        orderProcessorService.AddOrderService(customer, product, quantity);

                        Console.WriteLine("Want to add more product to cart then press one");

                        int addmore = int.Parse(Console.ReadLine());
                        while (addmore == 1)
                        {

                            switch (addmore)
                            {

                                case 1:
                                    Console.WriteLine("enter the customer id:");
                                    int morecustomerId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the product id:");
                                    int moreproductId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the quantity:");
                                    int morequantity = int.Parse(Console.ReadLine());
                                    customer = new Customer()
                                    {
                                        CustomerId = morecustomerId,
                                    };
                                    product = new Products()
                                    {
                                        ProductId = moreproductId,
                                    };
                                    orderProcessorService.AddOrderService(customer, product, morequantity);
                                    Console.WriteLine("do you want more then press 1 or to exit press 0");
                                    int over = int.Parse(Console.ReadLine());
                                    addmore = over;
                                    break;
                            }
                        }
                        break;
                    case 6:
                        Console.WriteLine("Enter the customer id:");
                        int deleteCustomerid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the product id:");
                        int deleteeproductid = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = deleteCustomerid,
                        };
                        product = new Products()
                        {
                            ProductId = deleteeproductid
                        };
                        orderProcessorService.DeleteFromCartService(customer, product);
                        break;
                    case 7:
                        Console.WriteLine("Enter Customer id:");
                        int cartcustomerid = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = cartcustomerid
                        };

                        orderProcessorService.DisplayCartService(customer);
                        break;
                    case 8:
                        Console.WriteLine("Enter Customer id:");
                        int Ordercustomerid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the product Details");
                        List<(Products, int)> productsAndQuantities1 = new List<(Products, int)>
                        {

                            (new Products {productId=int.Parse(Console.ReadLine())},2),


                        };
                        Console.WriteLine("Enter Shipping Address:");
                        string ShippingAddess = Console.ReadLine();
                        break;
                    case 9:
                        Console.WriteLine("Enter Customer Id:");
                        int customerId = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = customerId
                        };
                        orderProcessorService.ViewOrder(customerId);
                        break;
                    default:
                        Console.WriteLine("Enter a Valid Choice");
                        break;
                }

            }
        }
    }
}
