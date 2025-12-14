namespace Fcg.User.Domain
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task SaveAsync(User user);
        Task DeleteAsync(Guid id);
        Task UpdateWalletAsync(User user);
    }
}
