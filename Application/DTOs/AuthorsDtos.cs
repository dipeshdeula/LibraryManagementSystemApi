using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuthorsDtos
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public string AuthorProfile { get; set; } = null!;
        public IFormFile? AuthorImage { get; set; }
    }
}
