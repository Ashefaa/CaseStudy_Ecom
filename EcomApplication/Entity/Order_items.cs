using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Entity
{
    public class Order_items
    {
        public int orderItemId;
        public int? orderId;
        public int? productId;
        public int? quantity;

        public Order_items()
        { }

        public Order_items(int orderItemId, int orderId, int productId, int quantity)
        {
            this.orderItemId = orderItemId;
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
        }
        public int OrderItemId
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }
        public int? OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public int? ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public override string ToString()
        {
            return $"{OrderItemId} {OrderId} {ProductId} {Quantity}";
        }
    }
}
