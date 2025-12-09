namespace Server.Response;

using System.Text.Json.Serialization;

public class Pagination
{
    [JsonPropertyName("currentPage")]
    public required int CurrentPage { get; set; }

    private int _pageSize = 10;
    
    [JsonPropertyName("pageSize")]
    public required int PageSize {
        get
        {
            if (_pageSize <= 0) return 10;
            return _pageSize;
        } 
        set
        {
            _pageSize = value;
        }
    }

    [JsonPropertyName("totalItems")]
    public required int TotalItems { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages
    {
        get
        {
            if (PageSize == 0) return 0;
            return (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => CurrentPage > 1;

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => CurrentPage < TotalPages;
}