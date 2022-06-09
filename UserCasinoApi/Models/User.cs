namespace UserCasinoApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public double Balance { get; set; }
        public string? Token { get; set; }
    }
}
