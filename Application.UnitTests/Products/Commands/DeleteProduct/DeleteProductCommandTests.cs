using System.Threading;
using System.Threading.Tasks;
using Application.Common.Cloudinary;
using Application.Common.Exceptions;
using Application.Products.Commands.DeleteProductCommand;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Commands.DeleteProduct
{
    [TestFixture]
    public class DeleteProductCommandTests : CommandTestBase
    {
        [SetUp]
        public void SetUp()
        {
            var options = Options.Create(new CloudinarySettings
            {
                ApiKey = "unitTesting",
                ApiSecret = "UnitTesting",
                CloudName = "UnitTesting"
            });

            _handler = new DeleteProductCommand.DeleteProductCommandHandler(UnitOfWork, options);
        }

        private DeleteProductCommand.DeleteProductCommandHandler _handler;

        [Test]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteProductCommand {Id = 99};

            Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task Handle_GivenValidId_ShouldRemovePersistantProduct()
        {
            var command = new DeleteProductCommand {Id = 1};

            await _handler.Handle(command, CancellationToken.None);

            var entity = await UnitOfWork.Products.GetByIdAsync(command.Id);

            entity.ShouldBeNull();
        }
    }
}