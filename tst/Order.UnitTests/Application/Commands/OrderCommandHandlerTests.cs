using AutoFixture;
using Moq;
using Order.Commands;
using Order.Data.Entities;
using Order.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Order.UnitTests.Application.Commands
{
    public class OrderCommandHandlerTests
    {
        [Fact(DisplayName = "Fluxo da Criação do Pedido")]
        [Trait("Layer", "Application - Commands")]
        public async Task InputDataIsOk_Executed_CreateOrderReturningOrderdId()
        {
            // Arrange
            var createOrder = new Fixture().Create<CreateOrderCommand>();
            var repositoryMock = new Mock<IOrderRepository>();
            var commandHandler = new OrderCommandHandler(repositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(createOrder, new CancellationToken());

            // Assert
            Assert.True(result.IsValid() && result.Response != null);

            repositoryMock.Verify(pr => pr.Add(It.IsAny<OrderModel>()), Times.Once);
            repositoryMock.Verify(pr => pr.SaveAsync(), Times.Once);
        }

        [Fact(DisplayName = "Fluxo da Alteração do Pedido")]
        [Trait("Layer", "Application - Commands")]
        public async Task InputDataIsOk_Executed_ChangeOrder()
        {
            // Arrange
            var currentOrder = new Fixture().Create<OrderModel>();
            currentOrder.CalculateTotal();

            var updateOrder = new Fixture().Create<UpdateOrderCommand>();
            updateOrder.Id = currentOrder.Id;

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(pr => pr.GetByIdAsync(It.IsAny<int>()).Result).Returns(currentOrder);

            var commandHandler = new OrderCommandHandler(repositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(updateOrder, new CancellationToken());

            // Assert
            Assert.True(result.IsValid());
            repositoryMock.Verify(pr => pr.Update(It.IsAny<OrderModel>()), Times.Once);
            repositoryMock.Verify(pr => pr.SaveAsync(), Times.Once);
        }

        [Fact(DisplayName = "Fluxo da Exclusão do Pedido")]
        [Trait("Layer", "Application - Commands")]
        public async Task InputDataIsOk_Executed_DeleteOrder()
        {
            // Arrange
            var currentOrder = new Fixture().Create<OrderModel>();
            currentOrder.CalculateTotal();

            var deleteOrder = new Fixture().Create<DeleteOrderCommand>();
            deleteOrder.Id = currentOrder.Id;

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(pr => pr.GetByIdAsync(It.IsAny<int>()).Result).Returns(currentOrder);

            var commandHandler = new OrderCommandHandler(repositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(deleteOrder, new CancellationToken());

            // Assert
            Assert.True(result.IsValid());
            repositoryMock.Verify(pr => pr.Update(It.IsAny<OrderModel>()), Times.Once);
            repositoryMock.Verify(pr => pr.SaveAsync(), Times.Once);
        }

        [Fact(DisplayName = "Informado Usuário Inexistente")]
        [Trait("Layer", "Application - Commands")]
        public async Task UserInvalid_Executed_ResultInvalid()
        {
            // Arrange
            var currentOrder = new Fixture().Create<OrderModel>();
            currentOrder.CalculateTotal();

            var deleteOrder = new Fixture().Create<DeleteOrderCommand>();

            var repositoryMock = new Mock<IOrderRepository>();
            repositoryMock.Setup(pr => pr.GetByIdAsync(currentOrder.Id).Result).Returns(currentOrder);

            var commandHandler = new OrderCommandHandler(repositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(deleteOrder, new CancellationToken());

            // Assert
            Assert.False(result.IsValid());
            Assert.Equal(result.Errors.Count, 1);
            Assert.All(result.Errors, result => Assert.Contains(result, "Não foi possível localizar o atendimento!"));
        }
    }
}