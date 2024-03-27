using EcomApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Repositories
{
    public interface IOrderProcessorRepository
    {
        bool CreateProduct(Products product);
        bool CreateCustomer(Customer customer);
        bool DeleteProduct(int productId);
        bool DeleteCustomer(int customerId);
        bool AddToCart(Customer customer, Products products, int quantity);
        bool RemoveFromCart(Customer customer, Products products);
        List<Cart> GetAllFromCart(Customer customer);
        bool PlaceOrder(Customer customer, List<Dictionary<Products, int>> productsAndQuantities, string shippingAddress);
        List<Order_items> GetOrdersByCustomer(int customerId);

    }
}
