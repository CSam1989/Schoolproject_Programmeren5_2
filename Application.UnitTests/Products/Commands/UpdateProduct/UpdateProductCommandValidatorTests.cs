using System.Threading.Tasks;
using Application.Products.Commands.UpdateProductCommand;
using Domain.Enums;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidatorTests : CommandTestBase
    {
        private UpdateProductCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateProductCommandValidator(UnitOfWork);
        }

        [TestCase(1, true)]
        [TestCase(50, true)]
        [TestCase(51, false)]
        public void IsValid_ShouldBeExpectedResult_WhenNameLengthIsGiven(int nameLength, bool expected)
        {
            var name = new string('a', nameLength);

            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = name,
                Price = 1,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void IsValid_ShouldBeFalse_WhenNameIsEmpty(string name)
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = name,
                Price = 1,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        public void IsValid_ShouldBeExpectedResult_WhenPriceIsGiven(decimal price, bool expected)
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Test",
                Price = price,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [Test]
        public async Task IsValid_ShouldBeTrue_WhenIdAndNameIsSame()
        {
            var product = await UnitOfWork.Products.GetByIdAsync(1);

            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = product.Name,
                Price = 1,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Test]
        public async Task IsValid_ShouldBeFalse_WhenIdAndNameIsNotUnique()
        {
            var product = await UnitOfWork.Products.GetByIdAsync(1);

            var command = new UpdateProductCommand
            {
                Id = 2,
                Name = product.Name,
                Price = 1,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}