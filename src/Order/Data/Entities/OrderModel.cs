using Order.Data.DomainObjects;
using Order.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Data.Entities
{
    public class OrderModel : BaseEntity, IAggregateRoot
    {
        public OrderModel()
        {
        }

        public OrderModel(int customerId, ShippingType shipping, string observation)
        {
            CustomerId = customerId;
            Shipping = shipping;
            Observation = observation;
            StartedIn = DateTime.Now;
            OrderStatus = OrderStatusType.Analysis;
            Items = new List<OrderItemModel>();
        }

        public int CustomerId { get; set; }
        public DateTime StartedIn { get; set; }
        public DateTime? FinishedIn { get; set; }
        public OrderStatusType OrderStatus { get; set; }
        public ShippingType Shipping { get; set; }
        public decimal Total { get; set; }
        public string? Observation { get; set; }
        public List<OrderItemModel> Items { get; set; }

        public void DeliverOrder()
        {
            OrderStatus = OrderStatusType.Delivered;
            FinishedIn = DateTime.Now;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatusType.Finished;
        }

        public void UpdateItems(List<OrderItemModel> newItems)
        {
            var itemsToAdd = new List<OrderItemModel>();
            var itemsToRemove = new List<OrderItemModel>();

            foreach (var newItem in newItems)
            {
                var oldItem = Items.FirstOrDefault(i => i.ProductId == newItem.ProductId);

                if (oldItem == null)
                {
                    itemsToAdd.Add(newItem);
                }
                else
                {
                    oldItem.Update(newItem.Quantity, newItem.UnitPrice, newItem.Discount, newItem.DiscountValue);
                }
            }

            foreach (var oldItem in Items)
            {
                if (!newItems.Any(i => i.ProductId == oldItem.ProductId))
                {
                    itemsToRemove.Add(oldItem);
                }
            }

            foreach (var item in itemsToAdd)
                Items.Add(item);

            foreach (var item in itemsToRemove)
            {
                item.Delete();
                var itemToRemove = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
                Items.Remove(itemToRemove);
            }
        }

        public void CalculateTotal()
        {
            Total = Items.Sum(p => p.CalculateTotal());
        }

        public void Update(int customerId, ShippingType shipping, string observation)
        {
            CustomerId = customerId;
            Shipping = shipping;
            Observation = observation;
        }
    }
}