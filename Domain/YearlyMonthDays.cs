using System.ComponentModel.DataAnnotations.Schema;

namespace verticalSliceArchitecture.Domain
{
    [Table("tblYearlyMonthDays")]
    public class YearlyMonthDays
    {
        public int Id {  get; set; }
        public int Year {  get; set; }
        public int MonthId { get; set; }
        public NepaliMonth NepaliMonth { get; set; }
        public int DaysInMonth { get; set; }
    }
}
