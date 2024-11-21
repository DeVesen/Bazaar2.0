using System.Net;
using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Newtonsoft.Json;

namespace DeVesen.Bazaar.Client.Services;

public abstract class BaseService
{
    private readonly HttpClient _httpClient;

    public Uri? BaseAddress => _httpClient.BaseAddress;

    protected BaseService(IWebAssemblyHostEnvironment hostEnvironment,
        HttpClient httpClient,
        string controllerPath)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl(controllerPath);
    }

    protected async Task<(bool IsSuccess, T ResponsedData, string ErrorMsg)> GetFromJsonAsync<T>(string? requestUri) where T : class
    {
        var httpResponse = await _httpClient.GetAsync(requestUri);

        try
        {
            var responseData = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, JsonConvert.DeserializeObject<T>(responseData)!, null!);
            }
            else
            {
                return (false, null!, responseData);
            }
        }
        catch (Exception ex)
        {
            return (false, null!, ex.Message);
        }
    }

    protected async Task<(bool IsSuccess, HttpStatusCode StatusCode, string ErrorMsg)> PostAsync(string? requestUri, HttpContent? content = null)
    {
        var httpResponse = await _httpClient.PostAsync(requestUri, content);

        try
        {
            var responseData = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode
                ? (true, httpResponse.StatusCode, null!)
                : (false, httpResponse.StatusCode, responseData);
        }
        catch (Exception ex)
        {
            return (false, HttpStatusCode.BadRequest, $"Exception: {ex.Message}");
        }
    }

    protected async Task<(bool IsSuccess, HttpStatusCode StatusCode, string ErrorMsg)> PostAsJsonAsync<TValue>(string? requestUri, TValue value)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, value);

        try
        {
            var responseData = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode
                ? (true, httpResponse.StatusCode, null!)
                : (false, httpResponse.StatusCode, responseData);
        }
        catch (Exception ex)
        {
            return (false, HttpStatusCode.BadRequest, $"Exception: {ex.Message}");
        }
    }

    protected async Task<(bool IsSuccess, HttpStatusCode StatusCode, string ErrorMsg)> PutAsJsonAsync<TValue>(string? requestUri, TValue value)
    {
        var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, value);

        try
        {
            var responseData = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode
                ? (true, httpResponse.StatusCode, null!)
                : (false, httpResponse.StatusCode, responseData);
        }
        catch (Exception ex)
        {
            return (false, HttpStatusCode.BadRequest, $"Exception: {ex.Message}");
        }
    }

    protected async Task<(bool IsSuccess, HttpStatusCode StatusCode, string ErrorMsg)> DeleteAsync(string? requestUri)
    {
        var httpResponse = await _httpClient.DeleteAsync(requestUri);

        try
        {
            var responseData = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode
                ? (true, httpResponse.StatusCode, null!)
                : (false, httpResponse.StatusCode, responseData);
        }
        catch (Exception ex)
        {
            return (false, HttpStatusCode.BadRequest, $"Exception: {ex.Message}");
        }
    }


}