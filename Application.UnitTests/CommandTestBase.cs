using System;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Application.UnitTests
{
    public class CommandTestBase : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CommandTestBase()
        {
            _context = ApplicationDbContextFactory.Create();
            UnitOfWork = new UnitOfWork(_context);
        }

        public IUnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_context);
        }
    }
}