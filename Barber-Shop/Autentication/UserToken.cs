namespace Barber_Shop.Autentication
{
    public class UserToken
    {
        public bool IsAutenticated { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Message { get; set; }

    }
}
