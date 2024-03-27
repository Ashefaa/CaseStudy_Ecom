using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Entity
{
    public class Cart
    {
        public int cartId;
        public int? customerId;
        public int? productId;
        public int? quantity;

        public Cart()
        { }

        public Cart(int cartId, int customerId, int productId, int quantity)
        {
            this.cartId = cartId;
            this.customerId = customerId;
            this.productId = productId;
            this.quantity = quantity;
        }
        public int CartId
        {
            get { return cartId; }
            set { cartId = value; }
        }
        public int? CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
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
            return $"{CartId} {CustomerId} {ProductId} {Quantity}";
        }
    }
}
