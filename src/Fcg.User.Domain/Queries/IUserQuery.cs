using Fcg.User.Domain.Queries.Responses;

namespace Fcg.User.Domain.Queries
{
    public interface IUserQuery
    {
        Task<GetUserResponse> GetUserByIdAsync(Guid id);
        Task<IReadOnlyCollection<GetUserResponse>> GetUsersAsync();
    }
}
