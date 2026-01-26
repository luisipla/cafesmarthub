namespace CafeSmartHub.Client.Services;

public class ApiException : Exception
{
    public int StatusCode { get; }
    public string? Body { get; }

    public ApiException(string message, int statusCode, string? body = null) : base(message)
    {
        StatusCode = statusCode;
        Body = body;
    }
}
