using verticalSliceArchitecture.Domain;
using verticalSliceArchitecture.Features.Calendar.DTOs;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Calendar.Services.Interface
{
    public interface INepaliCalendarService
    {
        Task<PaginatedResponse<NepaliCalendarDto>> GetCalendarAsync(CalendarQueryParametersDTO parameters);
    }
}
