using System.Net.Http.Json;
using DeVesen.Bazaar.Shared;
using Microsoft.JSInterop;

namespace DeVesen.Bazaar.Client.Services;

public class AuthService(IJSRuntime jsRuntime, HttpClient httpClient)
{
    public async Task<bool> ConnectUser(LoginModel loginModel)
    {
        var result = await httpClient.PostAsJsonAsync("api/v1/Authentication/login", loginModel);

        if (!result.IsSuccessStatusCode)
        {
            return result.IsSuccessStatusCode;
        }

        var token = await result.Content.ReadAsStringAsync();
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

        return result.IsSuccessStatusCode;
    }

    public async Task DisconnectUser()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", null);
    }

    public async Task<string> GetAuthToken()
    {
        var tokenValue = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken", null);
        return tokenValue;
    }

    public async Task<bool> HasAuthToken()
    {
        return string.IsNullOrWhiteSpace(await GetAuthToken()) is false;
    }
}