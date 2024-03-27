using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Entity
{
    public class Products
    {

        public int productId;
        public string productname;
        public double price;
        public string description;
        public int? stockQuantity;

        public Products() { }
        public Products(int product, string productname, int price, string description, int stockQuantity)
        {
            this.productId = productId;
            this.productname = productname;
            this.price = price;
            this.description = description;
            this.stockQuantity = stockQuantity;
        }
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        public string ProductName
        {
            get { return productname; }
            set { productname = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int? StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }
        public override string ToString()
        {
            return $"{ProductId} {ProductName} {Price} {Description}{StockQuantity}";
        }
    }
}
