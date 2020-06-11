using System.Threading.Tasks;
using Application.Products.Commands.CreateProductCommand;
using Domain.Enums;
using NUnit.Framework;
using Shouldly;

namespace Application.UnitTests.Products.Commands.CreateProduct
{
    [TestFixture]
    public class CreateProductCommandValidatorTests : CommandTestBase
    {
        [SetUp]
        public void Setup()
        {
            _validator = new CreateProductCommandValidator(UnitOfWork);
        }

        private CreateProductCommandValidator _validator;

        [TestCase(1, true)]
        [TestCase(50, true)]
        [TestCase(51, false)]
        public void IsValid_ShouldBeExpectedResult_WhenNameLengthIsGiven(int nameLength, bool expected)
        {
            var name = new string('a', nameLength);

            var command = new CreateProductCommand
            {
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
            var command = new CreateProductCommand
            {
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
            var command = new CreateProductCommand
            {
                Name = "Test",
                Price = price,
                Category = Category.Fruit,
                Description = "Test"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [Test]
        public async Task IsValid_ShouldBeFalse_WhenNameIsNotUnique()
        {
            var product = await UnitOfWork.Products.GetByIdAsync(1);

            var command = new CreateProductCommand
            {
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