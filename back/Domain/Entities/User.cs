namespace Domain.Entities
{
    public class User
    {
        public string Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public string RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}
