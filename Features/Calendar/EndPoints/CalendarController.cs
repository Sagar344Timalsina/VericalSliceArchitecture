using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using verticalSliceArchitecture.Features.Calendar.DTOs;
using verticalSliceArchitecture.Features.Calendar.Queries;

namespace verticalSliceArchitecture.Features.Calendar.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "GetNepaliCalendar")]
        public async Task<IActionResult> GetNepaliCalendar([FromQuery] CalendarQueryParametersDTO queryParameters)
        {
            var result = await _mediator.Send(new GetNepaliCalendar.Query(queryParameters));
            return Ok(result);
        }
    }
}
