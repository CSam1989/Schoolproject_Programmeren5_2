using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Products.Queries.GetProductsByIdQuery;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest, IMapFrom<ProductByIdDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.Products.GetByIdAsync(request.Id);

                if (entity is null) throw new NotFoundException(nameof(Product), request.Id);

                entity.Name = request.Name;
                entity.Price = request.Price;
                entity.Category = request.Category;
                entity.Description = request.Description;
                entity.PhotoUrl = request.PhotoUrl;
                entity.PhotoId = request.PhotoId;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}