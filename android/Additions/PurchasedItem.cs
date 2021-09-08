using System;
namespace ExponeaSdk.Models
{
    public partial class PurchasedItem
    {
        public PurchasedItem(double value, string currency, string paymentSystem, string productId, string productTitle) : this(value, currency, paymentSystem, productId, productTitle, null)
        { }
    }
}
