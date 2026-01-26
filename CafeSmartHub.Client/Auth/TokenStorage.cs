using Microsoft.JSInterop;

namespace CafeSmartHub.Client.Auth;

public class TokenStorage
{
    private readonly IJSRuntime _js;
    public TokenStorage(IJSRuntime js) => _js = js;

    public ValueTask SetTokenAsync(string token)
        => _js.InvokeVoidAsync("localStorage.setItem", AuthConstants.TokenKey, token);

    public ValueTask<string?> GetTokenAsync()
        => _js.InvokeAsync<string?>("localStorage.getItem", AuthConstants.TokenKey);

    public ValueTask ClearAsync()
        => _js.InvokeVoidAsync("localStorage.removeItem", AuthConstants.TokenKey);
}
