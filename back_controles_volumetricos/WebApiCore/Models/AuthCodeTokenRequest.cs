namespace WebApiCore.Models
{
    public class AuthCodeTokenRequest
    {
        public string Code { get; set; }
        public string State { get; set; }
    }
}
