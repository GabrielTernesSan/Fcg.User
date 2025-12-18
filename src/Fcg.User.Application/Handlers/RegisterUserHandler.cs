using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Response>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var random = new Random();

            var name = $"User{random.Next()}";

            var user = new Domain.User(request.Id, name);

            await _userRepository.SaveAsync(user);

            return response;
        }
    }
}
