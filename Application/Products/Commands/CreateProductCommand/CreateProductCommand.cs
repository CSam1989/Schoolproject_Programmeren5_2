using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Category = request.Category,
                    Description = request.Description,
                    PhotoUrl = request.PhotoUrl,
                    PhotoId = request.PhotoId
                };

                _unitOfWork.Products.Create(entity);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}