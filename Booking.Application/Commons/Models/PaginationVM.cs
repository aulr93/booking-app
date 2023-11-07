namespace Booking.Application.Commons.Models
{
    public class PaginationResult<T>
    {
        public PaginationResult()
        {
            Data = new List<T>();
        }
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages
        {
            get { return TotalRecords % Size == 0 ? (int)(TotalRecords / Size) : ((int)TotalRecords / Size) + 1; }
        }
        public long TotalRecords { get; set; }
        public List<T> Data { get; set; }
    }
}
