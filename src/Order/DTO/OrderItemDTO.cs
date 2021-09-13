namespace Order.DTO
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public int Discount { get; set; }
        public decimal? DiscountValue { get; set; }
    }
}