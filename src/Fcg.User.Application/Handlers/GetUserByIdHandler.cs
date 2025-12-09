using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain.Queries;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, Response>
    {
        private readonly IUserQuery _userQuery;

        public GetUserByIdHandler(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }

        public async Task<Response> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _userQuery.GetUserByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                response.AddError($"Usuário {request.Id} não encontrado!");
                return response;
            }

            // TODO: Tem que implementar a lógica de buscar o usuário por ID no projeto Auth.

            var gameIds = user.Library == null ? [.. user.Library!.Select(g => g.Id)] : new List<Guid>();

            // TODO: Tem que pegar os jogos da biblioteca do usuário no projeto Game.

            return response;
        }
    }
}
