using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Products.Commands.UpdateProductCommand;
using Domain.Enums;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandTests : CommandTestBase
    {
        private UpdateProductCommand.UpdateProductCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new UpdateProductCommand.UpdateProductCommandHandler(UnitOfWork);
        }

        [Test]
        public async Task Handle_GivenValidId_ShouldUpdatePersistedProduct()
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Test",
                Price = 2,
                Category = Category.Vegetables,
                Description = "Test"
            };


            await _handler.Handle(command, CancellationToken.None);

            var entity = await UnitOfWork.Products.GetByIdAsync(command.Id);

            entity.ShouldNotBeNull();
            entity.Name.ShouldBe(command.Name);
            entity.Price.ShouldBe(command.Price);
            entity.Category.ShouldBe(command.Category);
            entity.Description.ShouldBe(command.Description);
        }

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpdateProductCommand
            {
                Id = 99,
                Name = "Test",
                Price = 2,
                Category = Category.Vegetables,
                Description = "Test"
            };

            Should.ThrowAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}