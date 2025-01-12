using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UsersCommand
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
