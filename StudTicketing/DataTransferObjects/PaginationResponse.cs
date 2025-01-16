namespace StudTicketing.DataTransferObjects;

public class PaginationResponse<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public List<T> Data { get; set; } = [];
}