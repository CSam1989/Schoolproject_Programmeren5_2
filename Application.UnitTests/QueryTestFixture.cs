using System;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Application.UnitTests
{
    public sealed class QueryTestFixture : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public QueryTestFixture()
        {
            _context = ApplicationDbContextFactory.Create();
            UnitOfWork = new UnitOfWork(_context);

            var configProvider = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            Mapper = configProvider.CreateMapper();
        }

        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; set; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_context);
        }
    }
}