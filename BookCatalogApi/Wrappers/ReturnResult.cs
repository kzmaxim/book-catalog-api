namespace BookCatalogApi.Wrappers;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public static ReturnResult<T> Success(T data) => new() { IsSuccess = true, Data = data };
    public static ReturnResult<T> Failure(string errorMessage) => new() { IsSuccess = false, ErrorMessage = errorMessage };
}