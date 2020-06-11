using System.Threading;
using System.Threading.Tasks;
using Application.Products.Commands.CreateProductCommand;
using Domain.Enums;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Commands.CreateProduct
{
    [TestFixture]
    public class CreateProductCommandTests : CommandTestBase
    {
        [SetUp]
        public void Setup()
        {
            _handler = new CreateProductCommand.CreateProductCommandHandler(UnitOfWork);
        }

        private CreateProductCommand.CreateProductCommandHandler _handler;

        [Test]
        public async Task Handle_ShouldPersistsProduct()
        {
            var command = new CreateProductCommand
            {
                Name = "Test",
                Price = 1,
                Category = Category.Dairy,
                Description = "This is a test"
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entity = await UnitOfWork.Products.GetByIdAsync(result);

            entity.ShouldNotBeNull();
            entity.Name.ShouldBe(command.Name);
            entity.Price.ShouldBe(command.Price);
            entity.Category.ShouldBe(command.Category);
            entity.Description.ShouldBe(command.Description);
        }
    }
}