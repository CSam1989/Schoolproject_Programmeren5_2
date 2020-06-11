using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Products.Queries.GetProductsByIdQuery;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Queries.GetProductById
{
    [TestFixture]
    public class GetProductByIdQueryTests
    {
        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();

            _handler = new GetProductByIdQuery.GetProductByIdQueryHandler(fixture.UnitOfWork, fixture.Mapper);
        }

        private GetProductByIdQuery.GetProductByIdQueryHandler _handler;

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var query = new GetProductByIdQuery {Id = 99};

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }

        [Test]
        public async Task Handle_GivenValidId_ReturnsCorrectVmAndObject()
        {
            var query = new GetProductByIdQuery {Id = 1};

            var result = await _handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ProductByIdVm>();

            var product = result.Product;
            product.ShouldBeOfType<ProductByIdDto>();
        }
    }
}