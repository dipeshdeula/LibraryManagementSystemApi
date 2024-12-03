using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.DTOs
{
    public class DateOnlyDto
    {

      

        public DateOnly Date { get; set; }

            public DateOnlyDto(DateOnly date)
            {
                Date = date;
            }

            // Optional: Add methods for formatting or parsing if needed
            public string ToFormattedString(string format = "yyyy-MM-dd")
            {
                return Date.ToString(format);
            }
        

    }
}
