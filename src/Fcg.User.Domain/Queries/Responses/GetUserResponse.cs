namespace Fcg.User.Domain.Queries.Responses
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Wallet { get; set; }
        public IReadOnlyCollection<GameResponse>? Library { get; set; }
    }
}
