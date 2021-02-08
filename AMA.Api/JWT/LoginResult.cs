namespace AMA.Api.JWT
{
    internal class LoginResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public object Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}