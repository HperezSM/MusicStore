namespace MusicStore.Dto.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
