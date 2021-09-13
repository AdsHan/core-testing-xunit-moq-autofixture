using AutoFixture;
using Moq;
using Order.Data.Entities;
using Order.DTO;
using Order.Queries;
using Order.Repositories;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Order.UnitTests.Application.Queries
{
    public class OrderQueryHandlerTests
    {

        [Fact]
        public async Task ThreeOrdersExist_Executed_ReturnThreeOrdersDTO()
        {
            // Arrange
            var Orders = new Fixture().Create<List<OrderModel>>();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(Orders);

            var getAllOrder = new Fixture().Create<GetAllOrderQuery>();

            var queryHandler = new OrderQueryHandler(repositoryMock.Object);

            // Act
            var OrdersList = await queryHandler.Handle(getAllOrder, new CancellationToken());

            // Assert
            Assert.NotNull(OrdersList);
            Assert.NotEmpty(OrdersList);
            Assert.Equal(OrdersList.Count, Orders.Count);

            var Model = Orders[0];
            var ItemModel = Orders[0].Items[0];

            var Dto = OrdersList[0];
            var ItemDto = OrdersList[0].Items[0];

            Dto.Id.ShouldBe(Dto.Id);
            Dto.CustomerId.ShouldBe(Dto.CustomerId);
            Dto.StartedIn.ShouldBe(Dto.StartedIn);
            Dto.FinishedIn.ShouldBe(Dto.FinishedIn);
            Dto.OrderStatus.ShouldBe(Dto.OrderStatus);
            Dto.Shipping.ShouldBe(Dto.Shipping);
            Dto.Total.ShouldBe(Dto.Total);
            Dto.Observation.ShouldBe(Dto.Observation);
            Dto.Items.Count.ShouldBe(Dto.Items.Count);
        }

        [Fact]
        public async Task OrderExist_Executed_ReturnOrderById()
        {
            // Arrange
            var Order = new Fixture().Create<OrderModel>();
            Order.CalculateTotal();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(pr => pr.GetByIdAsync(Order.Id).Result).Returns(Order);

            var getByIdOrder = new Fixture().Create<GetByIdOrderQuery>();
            getByIdOrder.Id = Order.Id;

            var OrderQueryHandler = new OrderQueryHandler(repositoryMock.Object);

            // Act
            var OrdersList = await OrderQueryHandler.Handle(getByIdOrder, new CancellationToken());

            // Assert
            Assert.NotNull(OrdersList);
            repositoryMock.Verify(pr => pr.GetByIdAsync(Order.Id).Result, Times.Once);
        }
    }
}