using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Entity
{
    public class Orders
    {
        public int orderId;
        public int? customerId;
        public DateOnly? date;
        public decimal? totalPrice;
        public string shippingAddress;

        public Orders()
        { }

        public Orders(int orderId, int customerId, DateOnly date, decimal totalPrice, string shopingAddress)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.date = date;
            this.totalPrice = totalPrice;
            this.shippingAddress = shopingAddress;
        }
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public DateOnly? Date
        {
            get { return date; }
            set { date = value; }
        }
        public decimal? TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        public string ShippingAddress
        {
            get { return shippingAddress; }
            set { shippingAddress = value; }
        }
        public override string ToString()
        {
            return $"{OrderId} {CustomerId} {Date} {TotalPrice} {ShippingAddress}";
        }
    }
}
