using System.Threading;
using System.Threading.Tasks;
using Application.Common.Cloudinary;
using Application.Common.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Photos.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IRequest<PhotoToReturnDto>
    {
        public IFormFile Photo { get; set; }

        public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, PhotoToReturnDto>
        {
            private readonly Cloudinary _cloudinary;

            public UploadPhotoCommandHandler(IOptions<CloudinarySettings> cloudinaryConfig)
            {
                var cloudinaryConfig1 = cloudinaryConfig;

                var acc = new Account(
                    cloudinaryConfig1.Value.CloudName,
                    cloudinaryConfig1.Value.ApiKey,
                    cloudinaryConfig1.Value.ApiSecret
                );

                _cloudinary = new Cloudinary(acc);
            }

            public async Task<PhotoToReturnDto> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
            {
                if (request.Photo is null)
                    throw new BadRequestException("No photo selected");

                ImageUploadResult uploadResult;

                if (request.Photo.Length > 0)
                {
                    await using (var stream = request.Photo.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(request.Photo.Name, stream),
                            Transformation = new Transformation()
                                .Width(400).Height(400)
                                .Crop("fill")
                        };

                        uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    }

                    var photoToReturn = new PhotoToReturnDto
                    {
                        PhotoId = uploadResult.PublicId,
                        PhotoUrl = uploadResult.Uri.ToString()
                    };

                    return photoToReturn;
                }

                throw new BadRequestException("Error at uploading photo");
            }
        }
    }
}