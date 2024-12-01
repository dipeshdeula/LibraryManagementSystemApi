using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuthorsEntity
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string Biography { get; set; } = null!;
        public string AuthorProfile { get; set; } = null!;
    }
}
