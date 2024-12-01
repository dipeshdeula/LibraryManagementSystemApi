using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateAuthorCommand : IRequest<int>
    {
        public UpdateAuthorCommand(int id, string authorName, string biography, string authorProfile, IFormFile authorImage)
        {
            AuthorId = id;
            AuthorName = authorName;
            Biography = biography;
            AuthorProfile = authorProfile;
            AuthorImage = authorImage;            
        }
        
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Biography { get; set; }
        public string AuthorProfile { get; set; }
        public IFormFile AuthorImage { get; set; }
    }
}
