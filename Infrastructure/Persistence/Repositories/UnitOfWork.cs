using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region fields

        private IRepository<Order> _orderRepo;
        private IRepository<OrderLine> _orderLineRepo;
        private IRepository<Product> _productRepo;
        private IRepository<Customer> _customerRepo;

        #endregion

        #region Ctor DI

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Repo Attributes

        public IRepository<Order> Orders => _orderRepo ??= new EFRepo<Order>(_context);

        public IRepository<OrderLine> OrderLines => _orderLineRepo ??= new EFRepo<OrderLine>(_context);

        public IRepository<Product> Products => _productRepo ??= new EFRepo<Product>(_context);

        public IRepository<Customer> Customers => _customerRepo ??= new EFRepo<Customer>(_context);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}