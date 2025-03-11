using MediatR;
using Microsoft.AspNetCore.Mvc;
using verticalSliceArchitecture.Features.Auth.Commands;
using verticalSliceArchitecture.Features.Auth.DTOs;

namespace verticalSliceArchitecture.Features.Auth.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IMediator _mediatr { get; set; }
        public IdentityController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> registerUser(RegisterRequestDto registerRequestDto)
        {
            var result = await _mediatr.Send(new CreateRegisterCommand(registerRequestDto));

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> loginUser(LoginRequestDto loginRequestDto)
        {
            var result = await _mediatr.Send(new CreateLoginCommand(loginRequestDto));

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("refersh-token")]
        public async Task<IActionResult> refreshToken([FromQuery]int UserId,[FromQuery]string RefreshToken)
        {
            var result = await _mediatr.Send(new CreateRefreshTokenCommand(UserId, RefreshToken));

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
