using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BooksCommand
{
    public class UpdateBookCommand : IRequest<int>
    {
        public UpdateBookCommand(int bookId, string? title = null, int? authorId = null, string? genre = null, string? iSBN = null, int? quantity = null, string? availabilityStatus = null)
        {
            BookId = bookId;
            Title = title;
            AuthorId = authorId;
            Genre = genre;
            ISBN = iSBN;
            Quantity = quantity;
            // PublishedDate = publishedDate;
            AvailabilityStatus = availabilityStatus;

        }
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public string? Genre { get; set; }
        public string? ISBN { get; set; }
        public int? Quantity { get; set; }
        // public DateOnly PublishedDate { get; set; }
        public string? AvailabilityStatus { get; set; }
    }
}
