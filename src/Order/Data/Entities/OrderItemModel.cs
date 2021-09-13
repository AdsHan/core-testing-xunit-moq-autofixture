using Order.Data.DomainObjects;
using Order.Data.Enum;

namespace Order.Data.Entities
{
    public class OrderItemModel : BaseEntity
    {
        public OrderItemModel(int orderId, int productId, int quantity, decimal unitPrice, DiscountType discount, decimal discountValue)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            DiscountValue = discountValue;
        }

        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DiscountType Discount { get; set; }
        public decimal DiscountValue { get; set; }

        public decimal CalculateTotal()
        {
            var discount = CalculateTotalDiscount();

            var total = (Quantity * UnitPrice) - discount;

            return total < 0 ? 0 : total;
        }

        internal decimal CalculateTotalDiscount()
        {
            if (DiscountValue == 0 || Discount == DiscountType.None) return 0;

            decimal discount = 0;

            if (Discount == DiscountType.Percentage)
            {
                discount = ((Quantity * UnitPrice) * DiscountValue) / 100;
            }
            else
            {
                discount = DiscountValue;
            }
            return discount;
        }

        public void Update(int quantity, decimal unitPrice, DiscountType discount, decimal discountValue)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            DiscountValue = discountValue;
        }
    }
}