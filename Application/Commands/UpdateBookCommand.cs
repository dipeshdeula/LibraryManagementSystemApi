using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateBookCommand : IRequest<int>
    {
        public UpdateBookCommand(int bookId,string title, int authorId, string genre, string iSBN, int quantity,DateOnly publishDate, string availabilityStatus)
        {
            BookId = bookId;
            Title = title;
            AuthorId = authorId;
            Genre = genre;
            ISBN = iSBN;
            Quantity = quantity;
            PublishDate = publishDate;
            AvailabilityStatus = availabilityStatus;

        }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public DateOnly PublishDate { get; set; }
        public string AvailabilityStatus { get; set; }
    }
}
