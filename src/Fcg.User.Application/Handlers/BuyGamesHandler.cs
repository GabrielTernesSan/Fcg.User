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

            var gameIds = request.GamesId;

            if (gameIds.Any())
            {
                foreach (var gameId in gameIds)
                {
                    var externalGameResponse = await _clientGame.GetGameAsync(gameId);

                    if (externalGameResponse.HasErrors)
                    {
                        response.AddError("Erro ao obter jogos.");
                    }
                    else
                    {
                        var gameResposne = externalGameResponse.Result;

                        var game = new Domain.Queries.Responses.GameResponse
                        {
                            Id = gameResposne.Id,
                            Title = gameResposne.Title,
                            Description = gameResposne.Description,
                            Price = gameResposne.Price,
                            Genre = gameResposne.Genre
                        };

                        if (game != null)
                        {
                            user.AddGameToLibrary(game.Id);
                        }
                    }
                }
            }

            await _userRepository.BuyGameAsync(user);

            return response;
        }
    }
}
