using System.Threading;
using System.Threading.Tasks;
using Application.Common.Cloudinary;
using Application.Common.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommand : IRequest

    {
        public string PhotoId { get; set; }

        public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand>
        {
            private readonly Cloudinary _cloudinary;

            public DeletePhotoCommandHandler(IOptions<CloudinarySettings> cloudinaryConfig)
            {
                var cloudinaryConfig1 = cloudinaryConfig;

                var acc = new Account(
                    cloudinaryConfig1.Value.CloudName,
                    cloudinaryConfig1.Value.ApiKey,
                    cloudinaryConfig1.Value.ApiSecret
                );

                _cloudinary = new Cloudinary(acc);
            }

            public async Task<Unit> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
            {
                if (request.PhotoId is null)
                    throw new BadRequestException("No photo selected");

                var deletionParams = new DeletionParams(request.PhotoId);

                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                if (deletionResult.Error is null)
                    return Unit.Value;

                throw new BadRequestException("Error at deleting photo");
            }
        }
    }
}