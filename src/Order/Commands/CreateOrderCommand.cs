using Order.Data.Enum;
using System.Collections.Generic;

namespace Order.Commands
{

    public class CreateOrderCommand : Command
    {
        public int CustomerId { get; set; }
        public ShippingType Shipping { get; set; }
        public string? Observation { get; set; }
        public List<CreateOrderItemCommand> Items { get; set; }

    }

    public class CreateOrderItemCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DiscountType Discount { get; set; }
        public decimal DiscountValue { get; set; }
    }

}