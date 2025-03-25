using System.ComponentModel.DataAnnotations.Schema;

namespace verticalSliceArchitecture.Domain
{
    [Table("tblNepaliCalendar")]
    public class NepaliCalendar
    {
        public int Id { get; set; }
        public int Year {  get; set; }
        public int MonthId { get; set; }
        public NepaliMonth NepaliMonth { get; set; }
        public int Day {  get; set; }
        public DateTime EnglishDate { get; set; }
        public string WeekDay {  get; set; }
        public bool IsHoliday {  get; set; }
        public string? HolidayName { get; set; }
    }
}
