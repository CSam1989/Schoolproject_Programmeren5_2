using System.Threading;
using System.Threading.Tasks;
using Application.Common.Cloudinary;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
            private readonly IUnitOfWork _unitOfWork;
            private readonly Cloudinary _cloudinary;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> cloudinaryConfig)
            {
                _unitOfWork = unitOfWork;
                _cloudinaryConfig = cloudinaryConfig;

                var acc = new Account(
                    _cloudinaryConfig.Value.CloudName,
                    _cloudinaryConfig.Value.ApiKey,
                    _cloudinaryConfig.Value.ApiSecret
                );

                _cloudinary = new Cloudinary(acc);
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _unitOfWork.Products.GetByIdAsync(request.Id);

                if (entity is null)
                    throw new NotFoundException(nameof(Product), request.Id);

                if (entity.PhotoId != null)
                {
                    var deletionParams = new DeletionParams(entity.PhotoId);

                    var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                    if (deletionResult.Error != null)
                        throw new BadRequestException("Fout bij verwijderen van foto");
                }

                _unitOfWork.Products.Delete(entity);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}