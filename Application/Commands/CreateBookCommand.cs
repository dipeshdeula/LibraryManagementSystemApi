using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{ 
    public class CreateBookCommand : IRequest<BooksEntity>
    {
        public CreateBookCommand(string title, int authorId, string genre, string iSBN,int quantity,DateOnly publishDate, string availabilityStatus)
        {
            Title = title;
            AuthorId = authorId;
            Genre = genre;
            ISBN = iSBN;
            Quantity = quantity;
            PublishDate = publishDate;
            AvailabilityStatus = availabilityStatus;

        }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public DateOnly PublishDate { get; set; }
        public string AvailabilityStatus{ get; set; }
    }
}
