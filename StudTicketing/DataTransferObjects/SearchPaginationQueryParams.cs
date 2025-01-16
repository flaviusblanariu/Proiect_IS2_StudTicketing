namespace StudTicketing.DataTransferObjects;

public class SearchPaginationQueryParams : PaginationQueryParams
{
    public string Search { get; set; }
}