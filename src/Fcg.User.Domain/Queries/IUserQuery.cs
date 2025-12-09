using Fcg.User.Domain.Queries.Responses;

namespace Fcg.User.Domain.Queries
{
    public interface IUserQuery
    {
        Task<GetUserResponse> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<GetUserResponse>> GetUsersAsync(CancellationToken cancellationToken);
    }
}
