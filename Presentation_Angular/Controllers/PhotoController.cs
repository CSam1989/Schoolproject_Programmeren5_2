using System.Threading.Tasks;
using Application.Photos.Commands.DeletePhoto;
using Application.Photos.Commands.UploadPhoto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<PhotoToReturnDto>> AddFoto([FromForm] IFormFile photo)
        {
            return await Mediator.Send(new UploadPhotoCommand {Photo = photo});
        }

        [HttpDelete("{photoId}")]
        public async Task<ActionResult> RemovePhoto(string photoId)
        {
            await Mediator.Send(new DeletePhotoCommand {PhotoId = photoId});

            return NoContent();
        }
    }
}