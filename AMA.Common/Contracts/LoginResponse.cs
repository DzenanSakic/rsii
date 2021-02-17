namespace AMA.Common.Contracts
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
