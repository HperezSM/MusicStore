namespace MusicStore.Dto.Request
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        private readonly int maxRecordsPerPage = 50;

        private int recordsPerPage = 10;

        public int RecordsPerPage
        {
            get { return recordsPerPage; }
            set {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }

    }
}
