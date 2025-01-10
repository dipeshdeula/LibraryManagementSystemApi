using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.DTOs
{
    public class BookDtos
    {
        public int BookId { get; set; }
        public string? Title { get; set; } 
        public int? AuthorId { get; set; } 
        public string? Genre { get; set; } 
        public string? ISBN { get; set; } 
        public int? Quantity { get; set; }


       // public string PublishedDate { get; set; } = string.Empty;
        public string? AvailabilityStatus { get; set; }

        //Utility to convert to/from DateOnly
       // public DateOnly GetPublishDateAsDateOnly() => DateOnly.ParseExact(PublishedDate, "yyyy-MM-dd");
       // public void SetPublishedDateFromDateOnly(DateOnly date) => PublishedDate = date.ToString("yyyy-MM-dd");
    }
}
