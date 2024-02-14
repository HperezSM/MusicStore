using MusicStore.Entities;

namespace MusicStore.Dto.Response
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }
        public string DateEvent { get; set; } = default!;
        public string TimeEvent { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = default!;
        public string OperationNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public short Quantity { get; set; }
        public string SaleDate { get; set; } = default!;
        public decimal Total { get; set; }
    }
}
