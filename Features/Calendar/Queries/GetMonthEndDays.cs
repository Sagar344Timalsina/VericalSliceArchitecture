using MediatR;
using verticalSliceArchitecture.Features.Calendar.DTOs;
using verticalSliceArchitecture.Features.Calendar.Services.Interface;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Calendar.Queries
{
    public record GetMonthEndDays(int YearId, int pageNumber, int pageSize) : IRequest<PaginatedResponse<MonthEndDaysResponseDto>>;

    public class GetMonthEndDaysHandler : IRequestHandler<GetMonthEndDays, PaginatedResponse<MonthEndDaysResponseDto>>
    {
        private readonly INepaliCalendarService _calendarService;
        public GetMonthEndDaysHandler(INepaliCalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        public async Task<PaginatedResponse<MonthEndDaysResponseDto>> Handle(GetMonthEndDays request, CancellationToken cancellationToken)
        {
            return await _calendarService.GetMonthEndDaysAsync(request.YearId,request.pageNumber,request.pageSize);

        }
    }
}
