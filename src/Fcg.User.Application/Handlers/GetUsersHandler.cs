using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain.Queries;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, Response>
    {
        private readonly IUserQuery _userQuery;

        public GetUsersHandler(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }

        public async Task<Response> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var users = await _userQuery.GetUsersAsync(cancellationToken);

            if (users == null)
            {
                response.AddError($"Usuários não encontrados!");
                return response;
            }

            // TODO: Tem que implementar a lógica de buscar os usuários por ID no projeto Auth.

            return response;
        }
    }
}
