using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, Response>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                response.AddError("Usuário não encontrado.");
                return response;
            }

            await _userRepository.DeleteAsync(user);

            // Manda email para auth para atualizar

            return response;
        }
    }
}
