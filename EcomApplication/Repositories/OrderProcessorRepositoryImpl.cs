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
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public OrderProcessorRepositoryImpl()
        {
            sqlConnection = new SqlConnection(DBConnection.GetConnectionString());
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
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            sqlConnection.Close();
            return true;
        }
        public bool CreateCustomer(Customer customer)
        {
            cmd.CommandText = "Insert into Customers values(@Name,@Email,@Password)";
            //cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Password", customer.Password);
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            sqlConnection.Close();
            return true;
        }
        public bool DeleteProduct(int productId)
        {
            cmd.CommandText = "Delete from Product where product_id=@ProductId";
             cmd.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            sqlConnection.Close();
            return true;

        }

        public bool DeleteCustomer(int customerId)
        {
            cmd.Connection = sqlConnection;
            cmd.CommandText = "Delete from Customers where customer_id=@CustomerId";
            // cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return true;
        }
        public bool AddToCart(Customer customer, Products products, int quantity)
        {
            cmd.CommandText = "Insert into Cart values(@CustomerId,@ProductId,@Quantity";

            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", products.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            sqlConnection.Close();
            return true;
        }
        public bool RemoveFromCart(Customer customer, Products products)
        {
            cmd.CommandText = "Delete from Cart where customer_id=@CustomertId AND product_id=@ProductId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            cmd.Parameters.AddWithValue("@ProductId", products.ProductId);
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return true;
        }
        public List<Products> GetAllFromCart(Customer customer)
        {
            List<Products> products = new List<Products>();
            cmd.CommandText = "Select p.product_id,p.name,p.price,p.description,p.stockQuantity From Products p Join Cart c ON p.product_id=c.product_id Where c.customer_id=@CustomerId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Products product = new Products();

                product.ProductId = (int)reader["product_id"];
                product.ProductName = (string)reader["name"];
                product.Price = Convert.ToDouble(reader["price"]);
                product.StockQuantity = Convert.ToInt32(reader["quantity"]);

                products.Add(product);
            }
            sqlConnection.Close();
            return products;
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
        public List<Dictionary<Products, int>> GetOrdersByCustomer(int customerId)
        {
            List<Dictionary<Products, int>> orders = new List<Dictionary<Products, int>>();
            cmd.CommandText = "Select oi.order_id,oi.product_id,oi.quantity From order_items oi join Products p on oi.product_id=p.product_id join orders o on oi.order_id=o.order_id where o.customer_id=@CustomerId";
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Products products = new Products
                {
                    ProductId = (int)reader["product_id"],
                    ProductName = (string)reader["name"],
                    price = Convert.ToDouble(reader["price"])
                };
                int StockQuantity = Convert.ToInt32(reader["quantity"]);
                Dictionary<Products, int> orderItem = new Dictionary<Products, int>();
                orderItem.Add(products, StockQuantity);

            }
            return orders;
           
        }
        public bool CustomerNotPresent(int customerId)
        {
            int noofcustomer = 0;
            cmd.CommandText = "Select count(*)as total from Customers where customer_id=@CustomerId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noofcustomer = (int)(reader["total"]);

            }
            sqlConnection.Close();
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
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noOfProduct = (int)(reader["total"]);

            }
            sqlConnection.Close();
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
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                noOfOrder = (int)(reader["total"]);

            }
            sqlConnection.Close();
            if (noOfOrder > 0)
            {
                return true;
            }
            return false;
        }
    }
}
