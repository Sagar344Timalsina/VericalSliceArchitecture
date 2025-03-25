using System.ComponentModel.DataAnnotations;

namespace verticalSliceArchitecture.Features.Calendar.DTOs
{
    public class CalendarQueryParametersDTO
    {
        [Range(1, 3000, ErrorMessage = "Year must be between 1 and 3000")]
        public int Year { get; set; }
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }
        public bool ShowHolidaysOnly { get; set; } = false;
        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be at least 1")]
        public int PageNumber { get; set; } = 1;

        public string SortBy { get; set; } = "Day"; 
        public bool Descending { get; set; } = false;
    }
}
