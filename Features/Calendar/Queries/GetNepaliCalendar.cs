using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using verticalSliceArchitecture.Data;
using verticalSliceArchitecture.Domain;
using verticalSliceArchitecture.Features.Calendar.DTOs;
using verticalSliceArchitecture.Features.Calendar.Services.Interface;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Calendar.Queries
{
    public class GetNepaliCalendar
    {
        public class Query : IRequest<PaginatedResponse<NepaliCalendarDto>>
        {
            public CalendarQueryParametersDTO Parameters { get; set; }

            public Query(CalendarQueryParametersDTO parameters)
            {
                Parameters = parameters;
            }
        }
        public class Handler : IRequestHandler<Query, PaginatedResponse<NepaliCalendarDto>>
        {
            private readonly INepaliCalendarService _calendarService;
           
            public Handler(INepaliCalendarService calendarService )
            {
                _calendarService = calendarService;
            
            }
            public async Task<PaginatedResponse<NepaliCalendarDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response= await _calendarService.GetCalendarAsync(request.Parameters);
              
                return response;
            }
        }
    }
}
