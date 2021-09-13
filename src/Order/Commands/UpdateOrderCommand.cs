using Order.Data.Enum;
using System.Collections.Generic;

namespace Order.Commands
{
    public class UpdateOrderCommand : Command
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ShippingType Shipping { get; set; }
        public string? Observation { get; set; }
        public List<UpdateOrderItemCommand> Items { get; set; }

    }

    public class UpdateOrderItemCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DiscountType Discount { get; set; }
        public decimal DiscountValue { get; set; }
    }

}