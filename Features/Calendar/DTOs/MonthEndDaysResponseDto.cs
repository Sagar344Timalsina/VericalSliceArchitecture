using verticalSliceArchitecture.Domain;

namespace verticalSliceArchitecture.Features.Calendar.DTOs
{
    public class MonthEndDaysResponseDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int MonthId { get; set; }
        public string MonthName{ get; set; }
        public int DaysInMonth { get; set; }
    }
}
