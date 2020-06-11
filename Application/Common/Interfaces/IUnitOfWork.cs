using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Order> Orders { get; }
        public IRepository<OrderLine> OrderLines { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}