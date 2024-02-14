namespace MusicStore.Entities
{
    public class Customer : EntityBase
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}
