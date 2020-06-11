using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Products.Queries.GetProductsQuery;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Queries.GetProducts
{
    [TestFixture]
    public class GetProductsQueryTests
    {
        [SetUp]
        public void SetUp()
        {
            var fixture = new QueryTestFixture();

            _handler = new GetProductsQuery.GetProductsQueryHandler(fixture.UnitOfWork, fixture.Mapper);
        }

        private GetProductsQuery.GetProductsQueryHandler _handler;

        [Test]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new GetProductsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<ProductsVm>();
            result.List.Count.ShouldBe(3);

            var product = result.List.First();

            product.ShouldBeOfType<ProductListDto>();
            product.Id.ShouldBe(1);
        }
    }
}