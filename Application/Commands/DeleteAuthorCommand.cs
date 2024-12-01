using MediatR;

namespace Application.Commands
{
    public class DeleteAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
