using EcomApplication.Entity;
using EcomApplication.Utility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Repositories
{
    public class OrderProcessorRepositoryImpl:IOrderProcessorRepository
    {
        SqlConnection connect = null;
        SqlCommand cmd = null;

        public OrderProcessorRepositoryImpl()
        {
            connect = new SqlConnection(DBConnection.GetConnectionString());
            cmd = new SqlCommand();
        }

        Customer customer = new Customer();
        Products products = new Products();
        Orders orders = new Orders();

        public bool CreateProduct(Products product)
        {

            cmd.CommandText = "Insert into Product values(@Name,@Price,@Description,@Quantity) ";

            cmd.Parameters.AddWithValue("@Name", product.ProductName);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("Quantity", product.StockQuantity);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connect.Close();
            return true;
        }
        public bool CreateCustomer(Customer customer)
        {
            cmd.CommandText = "Insert into Customers values(@Name,@Email,@Password)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connect.Close();
            return true;
        }
        public bool DeleteProduct(int productId)
        {
            cmd.CommandText = "Delete from Product where product_id=@ProductId";
             cmd.Parameters.AddWithValue("@ProductId", productId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            connect.Close();
            return true;

        }

        public bool DeleteCustomer(int customerId)
        {
            cmd.Connection = connect;
            cmd.CommandText = "Delete from Customers where customer_id=@CustomerId";
            // cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
            return true;
        }
        public bool AddToCart(Customer customer, Products products, int quantity)
        {
            cmd.CommandText = "Insert into Cart values(@CustomerId,@ProductId,@Quantity";

            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", products.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connect.Close();
            return true;
        }
        public bool RemoveFromCart(Customer customer, Products products)
        {
            cmd.CommandText = "Delete from Cart where customer_id=@CustomertId AND product_id=@ProductId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", products.ProductId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
            return true;
        }
        public List<Cart> GetAllFromCart(Customer customer)
        {
            List<Cart> cartList = new List<Cart>();
            cmd.CommandText = "Select * from cart where customer_id=@CustomerId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cart cart = new Cart();

                cart.CartId = (int)reader["cart_id"];
                cart.CustomerId = Convert.IsDBNull(reader["customer_id"]) ? null : (int)reader["customer_id"];
                cart.ProductId = Convert.IsDBNull(reader["product_id"]) ? null : (int)reader["product_id"];
                cart.Quantity = Convert.IsDBNull(reader["quantity"]) ? null : (int)reader["quantity"];

                cartList.Add(cart);
            }
            connect.Close();
            return cartList;
        }
        public bool PlaceOrder(Customer customer, List<Dictionary<Products, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                decimal totalPrice = 0;
                int orderId;
                //Insert a new record in orders table
                cmd.CommandText = "Insert into Orders values(@CustomerId,GETDATE(),@TotalPrice,@ShippingAddress)";
                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                //insert order items into orders item table and calculate the total amount
                cmd.CommandText = "Insert into order_items values(@OrderId,@ProductId,@Quantity)";
                foreach (var item in productsAndQuantities)
                {
                    foreach (var keyValuePair in item)
                    {
                        
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", keyValuePair.Key.productId);
                        cmd.Parameters.AddWithValue("@Quantity", keyValuePair.Value);
                        cmd.ExecuteNonQuery();
                    }

                }
                //update stock quantity in product table
                cmd.CommandText = "Update Products SET stockQuantity=stockQuantity-@Quantity where product_id=@ProductId";
                foreach (var item in productsAndQuantities)
                {
                    foreach (var keyValuePair in item)
                    {
                        cmd.Parameters.AddWithValue("@Quantity", keyValuePair.Value);
                        cmd.Parameters.AddWithValue("@ProductId", keyValuePair.Key.productId);
                        cmd.ExecuteNonQuery();

                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error placing Order" + ex.Message);
                return false;
            }
        }
        public List<Order_items> GetOrdersByCustomer(int customerId)
        {
            List<Order_items> orders = new List<Order_items>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = "Select oi.order_id,oi.product_id,oi.quantity From order_items oi join Products p on oi.product_id=p.product_id join orders o on oi.order_id=o.order_id where o.customer_id=@CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order_items order_Items = new Order_items();
                {
                    order_Items.OrderItemId = (int)reader["product_id"];
                    order_Items.ProductId = (int)reader["productId"];
                    order_Items.Quantity = (int)reader["quantity"];
                    //price = Convert.ToDecimal(reader["price"])
                };
                int StockQuantity = Convert.ToInt32(reader["quantity"]);
               
                orders.Add(order_Items);

            }
            return orders;
        }
        public bool CustomerNotPresent(int customerId)
        {
            int noofcustomer = 0;
            cmd.CommandText = "Select count(*)as total from Customers where customer_id=@CustomerId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noofcustomer = (int)(reader["total"]);

            }
            connect.Close();
            if (noofcustomer > 0)
            {
                return true;
            }
            return false;
        }
        public bool ProductNotFound(int productId)
        {
            int noOfProduct = 0;
            cmd.CommandText = "select count(*) as total from products where product_id=@ProductId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ProductId", productId);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noOfProduct = (int)(reader["total"]);

            }
            connect.Close();
            if (noOfProduct > 0)
            {
                return true;
            }
            return false;
        }
        public bool OrderNotExist(int orderId)
        {
            int noOfOrder = 0;
            cmd.CommandText = "select count(*) as total from products where order_id=@OrderId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noOfOrder = (int)(reader["total"]);

            }
            connect.Close();
            if (noOfOrder > 0)
            {
                return true;
            }
            return false;
        }
    }
}
