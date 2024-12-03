﻿using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand,BooksEntity>
    {
        private readonly IBookService _bookService;

        public CreateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<BooksEntity> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new BooksEntity
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                Genre = request.Genre,
                ISBN = request.ISBN,
                Quantity = request.Quantity,
                PublishDate =DateOnly.FromDateTime(DateTime.Today), // Set the current date
                AvailabilityStatus = request.AvailabilityStatus
            };

            return await _bookService.CreateBooksAsync(book);
        }
    }
}