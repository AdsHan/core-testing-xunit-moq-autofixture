using MediatR;
using MOP.Core.Commands;
using Order.Communication;
using Order.Data.Entities;
using Order.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Commands
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<CreateOrderCommand, BaseResult>,
        IRequestHandler<UpdateOrderCommand, BaseResult>,
        IRequestHandler<DeleteOrderCommand, BaseResult>
    {

        private readonly IOrderRepository _orderRepository;

        private int idEvent;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<BaseResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = new OrderModel(command.CustomerId, command.Shipping, command.Observation);

            var items = command.Items.Select(i => new OrderItemModel(order.Id, i.ProductId, i.Quantity, i.UnitPrice, i.Discount, i.DiscountValue)).ToList();
            order.UpdateItems(items);

            order.CalculateTotal();

            _orderRepository.Add(order);

            try
            {
                await _orderRepository.SaveAsync();

                BaseResult.Response = order.Id;
            }
            catch (Exception ex)
            {
                AddError("Erro ao publicar menssagem");
            }

            return BaseResult;
        }

        public async Task<BaseResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);

            if (order == null)
            {
                AddError("Não foi possível localizar o atendimento!");
                return BaseResult;
            }

            order.Update(command.CustomerId, command.Shipping, command.Observation);

            var newItems = command.Items.Select(i => new OrderItemModel(order.Id, i.ProductId, i.Quantity, i.UnitPrice, i.Discount, i.DiscountValue)).ToList();

            order.UpdateItems(newItems);

            order.CalculateTotal();

            _orderRepository.Update(order);

            try
            {
                await _orderRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                AddError("Erro ao salvar o atendimento");
            }

            return BaseResult;
        }

        public async Task<BaseResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);

            if (order == null)
            {
                AddError("Não foi possível localizar o atendimento!");
                return BaseResult;
            }

            order.Delete();

            _orderRepository.Update(order);

            await _orderRepository.SaveAsync();

            return BaseResult;
        }

    }
}