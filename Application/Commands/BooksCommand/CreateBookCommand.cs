using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BooksCommand
{
    public class CreateBookCommand : IRequest<BooksEntity>
    {
        public CreateBookCommand(string title, int? authorId, string genre, string iSBN, int? quantity, string availabilityStatus)
        {
            Title = title;
            AuthorId = authorId;
            Genre = genre;
            ISBN = iSBN;
            Quantity = quantity;
            // PublishedDate = publishedDate;
            AvailabilityStatus = availabilityStatus;

        }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int? Quantity { get; set; }
        //  public DateOnly PublishedDate { get; set; }
        public string AvailabilityStatus { get; set; }
    }
}
