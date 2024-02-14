namespace MusicStore.Entities
{
    public class Concert : EntityBase
    {        
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public DateTime DateEvent { get; set; }
        public int TicketsQuantity { get; set; }
        public bool Finalized { get; set; }
        public int GenreId { get; set; }
        public string? ImageUrl { get; set; }

        //Navigation Properties
        public Genre Genre { get; set; } = default!;
    }
}
