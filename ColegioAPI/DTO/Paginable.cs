namespace ColegioAPI.DTO
{
    public class Paginable<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
