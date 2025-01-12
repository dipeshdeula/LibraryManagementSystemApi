using MediatR;

namespace Application.Commands.Author
{
    public class DeleteAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
