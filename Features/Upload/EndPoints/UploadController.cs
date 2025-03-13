using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using verticalSliceArchitecture.Features.Upload.Commands;

namespace verticalSliceArchitecture.Features.Upload.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController(IMediator _mediatr) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> uploadImage([FromForm] IFormFile file)
        {
            var command = new UploadCommands { File = file };
            var result = await _mediatr.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
