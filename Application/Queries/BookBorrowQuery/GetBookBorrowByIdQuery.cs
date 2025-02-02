﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.BookBorrowQuery
{
    public record GetBookBorrowByIdQuery(int id) : IRequest<BookBorrowEntity>;

}
