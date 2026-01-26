using System.Net.Http.Headers;

namespace CafeSmartHub.Client.Auth;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenStorage _tokenStorage;
    public AuthHeaderHandler(TokenStorage tokenStorage) => _tokenStorage = tokenStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
