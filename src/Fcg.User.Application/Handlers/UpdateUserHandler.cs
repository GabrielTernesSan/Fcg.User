using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, Response>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                response.AddError("Usuário não encontrado.");
                return response;
            }

            user.Update(request.Name);

            await _userRepository.SaveAsync(user);

            // Manda email para auth para atualizar

            return response;
        }
    }
}
