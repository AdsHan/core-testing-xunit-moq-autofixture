using Order.Data.Entities;
using Order.Data.Enum;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Order.UnitTests.Domain
{
    public class OrderTests
    {

        private OrderModel _Order;

        public OrderTests()
        {
            var items = new List<OrderItemModel>() {
                new OrderItemModel(1254, 13544, 1, (decimal)1000.00, 0, (decimal)200.00),
                new OrderItemModel(1254, 13555, 2, (decimal)2000.00, 0, (decimal)200.00),
                new OrderItemModel(1254, 13566, 3, (decimal)3000.00, 0, (decimal)200.00)
            };

            _Order = new OrderModel(1254, ShippingType.FOB, "Cliente irá retirar na loja");

            _Order.UpdateItems(items);
            _Order.CalculateTotal();
        }

        [Fact]
        public void CreateOrder()
        {
            Assert.Equal(EntityStatusEnum.Active, _Order.Status);
            Assert.NotNull(_Order.DateCreateAt);

            Assert.NotNull(_Order.Id);
            Assert.NotNull(_Order.CustomerId);

            Assert.NotNull(_Order.Observation);
            Assert.NotEmpty(_Order.Observation);

            Assert.Equal(_Order.Items.Count, 3);

            foreach (var item in _Order.Items)
            {
                Assert.Equal(EntityStatusEnum.Active, item.Status);
                Assert.NotNull(item.DateCreateAt);
                Assert.NotNull(item.OrderId);
                Assert.NotNull(item.ProductId);
                Assert.NotNull(item.Quantity);
                Assert.NotNull(item.UnitPrice);
            }

            Assert.Equal(_Order.Total, (decimal)14000.00);
        }

        [Fact]
        public void UpdateOrder()
        {
            var newItems = new List<OrderItemModel>() {
                new OrderItemModel(1254, 13555, 1, (decimal)2000.00, 0, (decimal)200.00),
                new OrderItemModel(1254, 13566, 1, (decimal)3000.00, 0, (decimal)200.00)
            };

            _Order.UpdateItems(newItems);
            _Order.CalculateTotal();

            bool isProductExist = _Order.Items.Any(i => i.ProductId == 13544);
            Assert.False(isProductExist);

            Assert.Equal(_Order.Items.Count, newItems.Count);
            Assert.Equal(_Order.Total, (decimal)5000.00);
        }

        [Fact]
        public void DeleteOrder()
        {
            _Order.Delete();

            Assert.Equal(EntityStatusEnum.Inactive, _Order.Status);
            Assert.NotNull(_Order.DateDeleteAt);
        }

        [Theory]
        [MemberData(nameof(DataItems))]
        public void CalculateTotal(List<OrderItemModel> items, decimal expected)
        {
            _Order.UpdateItems(items);
            _Order.CalculateTotal();

            Assert.Equal(_Order.Total, expected);
        }

        public static IEnumerable<object[]> DataItems =>
               new List<object[]>
               {
                    new object[] { new List<OrderItemModel>() {
                                   new OrderItemModel(1254, 13555, 1, (decimal)2000.00, 0, (decimal)200.00),
                                   new OrderItemModel(1254, 13566, 1, (decimal)3000.00, 0, (decimal)200.00)}, (decimal)5000.00},
                    new object[] { new List<OrderItemModel>() {
                                   new OrderItemModel(1254, 13566, 1, (decimal)3000.00, DiscountType.Value, (decimal)200.00)}, (decimal)2800.00},
                    new object[] { new List<OrderItemModel>() {
                                   new OrderItemModel(1254, 13555, 2, (decimal)2000.00, DiscountType.Value, (decimal)200.00),
                                   new OrderItemModel(1254, 13566, 2, (decimal)3000.00, DiscountType.Value, (decimal)5.00)}, (decimal)9795.00},
                    new object[] { new List<OrderItemModel>() {
                                   new OrderItemModel(1254, 13555, 1, (decimal)2000.00, DiscountType.Percentage, (decimal)5.00),
                                   new OrderItemModel(1254, 13566, 1, (decimal)3000.00, DiscountType.Percentage, (decimal)5.00)}, (decimal)4750.00},
               };
    }
}
