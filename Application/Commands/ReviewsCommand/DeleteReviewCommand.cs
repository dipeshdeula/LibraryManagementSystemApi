using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ReviewsCommand
{
    public class DeleteReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
