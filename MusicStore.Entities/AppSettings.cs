using System.Diagnostics;

namespace MusicStore.Entities
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }
        public SmtpConfiguration SmtpConfiguration { get; set; } = default!;
    }

    public class Jwt
    {
        public string JWTKey { get; set; }
        public int LifetimeInSeconds { get; set; }
    }
    public class SmtpConfiguration
    {
        public string UserName { get; set; } = default!;
        public string Server { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int PortNumber { get; set; }
        public string FromName { get; set; } = default!;
        public bool EnableSsl { get; set; }
    }
}
