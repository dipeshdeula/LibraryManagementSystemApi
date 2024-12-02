using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DateOnlyDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        //parameterless constructor
        public DateOnlyDto() { }

        public DateOnlyDto(DateOnly date)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }

        public DateOnly ToDateOnly()
        {
            return new DateOnly(Year, Month, Day);
        }
        public override string ToString()
        {
            return $"{Year:D4}-{Month:D2}-{Day:D2}";
        }
    }
}
