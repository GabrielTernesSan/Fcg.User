namespace Fcg.User.Domain.Queries.Responses
{
    public class GameResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
