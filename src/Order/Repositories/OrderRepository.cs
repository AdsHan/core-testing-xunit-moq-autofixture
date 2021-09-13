using Order.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {

        }

        public async Task<List<OrderModel>> GetAllAsync()
        {
            return new List<OrderModel>();
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            return new OrderModel();
        }

        public void Update(OrderModel order)
        {

        }

        public void Add(OrderModel order)
        {
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
