using Order.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Repositories
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task SaveAsync();
        void Update(T obj);
        void Add(T obj);
    }
}