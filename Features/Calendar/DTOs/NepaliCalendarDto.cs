namespace verticalSliceArchitecture.Features.Calendar.DTOs
{
    public class NepaliCalendarDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateTime EnglishDate { get; set; }
        public string WeekDay { get; set; }
        public bool IsHoliday { get; set; }
        public string HolidayName { get; set; }
        public string NepaliMonthName { get; set; }
    }
}
