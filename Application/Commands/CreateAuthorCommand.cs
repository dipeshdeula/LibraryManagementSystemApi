﻿using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateAuthorCommand : IRequest<AuthorsEntity>
    {
        public CreateAuthorCommand(string authorName, string biography, string authorProfile, IFormFile authorImage)
        {
            AuthorName = authorName;
            Biography = biography;
            AuthorProfile = authorProfile;
            AuthorImage = authorImage;
            
        }

        public string AuthorName { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public string AuthorProfile { get; set; } = null!;
        public IFormFile AuthorImage { get; set; }

    }
}
