using System.ComponentModel.DataAnnotations.Schema;

namespace verticalSliceArchitecture.Domain
{
    [Table("tblNepaliMonth")]
    public class NepaliMonth
    {
        public int Id { get; set; }
        public int MonthNumber {  get; set; }
        public string MonthName {  get; set; }

        public ICollection<NepaliCalendar> NepaliCalendars { get; set; }
        public ICollection<YearlyMonthDays> YearlyMonthDays { get; set; }
    }
}
