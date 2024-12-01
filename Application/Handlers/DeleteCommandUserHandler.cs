using Application.Commands;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteCommandUserHandler : IRequestHandler<DeleteUserCommand,int>
    {
        private readonly IUserService _userService;

        public DeleteCommandUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(request.Id);
            return request.Id;
        }
    }
}
