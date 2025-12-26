using Fcg.User.Application.Requests;
using Fcg.User.Common;
using Fcg.User.Domain;
using Fcg.User.Proxy.Games.Client.Interface;
using MediatR;

namespace Fcg.User.Application.Handlers
{
    public class BuyGamesHandler : IRequestHandler<BuyGamesRequest, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientGames _clientGame;

        public BuyGamesHandler(IClientGames clientGame, IUserRepository userRepository)
        {
            _clientGame = clientGame;
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(BuyGamesRequest request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                response.AddError("Usuário não encontrado.");
                return response;
            }

            if (request.GamesId != null && request.GamesId.Any())
            {
                var externalGameResponse = await _clientGame.GetGamesAsync(request.GamesId);

                if (externalGameResponse.HasErrors || externalGameResponse.Result == null)
                {
                    response.AddError("Erro ao obter informações dos jogos externos.");
                    return response;
                }

                foreach (var gameData in externalGameResponse.Result)
                {
                    user.AddGameToLibrary(gameData.Id);
                }

                try
                {
                    await _userRepository.BuyGameAsync(user);
                }
                catch (Exception ex)
                {
                    response.AddError($"Erro ao salvar compra: {ex.Message}");
                }
            }

            return response;
        }
    }
}
