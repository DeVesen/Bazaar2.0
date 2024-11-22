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


    protected async Task<ApiResponse<TResponseData>> GetFromJsonAsync<TResponseData>(string? requestUri) where TResponseData : class
    {
        var httpResponse = await _httpClient.GetAsync(requestUri);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }


    protected async Task<ApiResponse> PostAsync(string? requestUri, HttpContent? content = null)
    {
        var httpResponse = await _httpClient.PostAsync(requestUri, content);

        try
        {
            return await httpResponse.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ApiResponse.ByException(ex);
        }
    }

    protected async Task<ApiResponse<TResponseData>> PostAsync<TResponseData>(string? requestUri, HttpContent? content = null) where TResponseData : class
    {
        var httpResponse = await _httpClient.PostAsync(requestUri, content);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }


    protected async Task<ApiResponse> PostAsJsonAsync<TValue>(string? requestUri, TValue value)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, value);

        try
        {
            return await httpResponse.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ApiResponse.ByException(ex);
        }
    }

    protected async Task<ApiResponse<TResponseData>> PostAsJsonAsync<TActionData, TResponseData>(string? requestUri, TActionData value) where TResponseData : class
    {
        var httpResponse = await _httpClient.PostAsJsonAsync(requestUri, value);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }


    protected async Task<ApiResponse> PutAsync(string? requestUri, HttpContent? content = null)
    {
        var httpResponse = await _httpClient.PutAsync(requestUri, content);

        try
        {
            return await httpResponse.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ApiResponse.ByException(ex);
        }
    }

    protected async Task<ApiResponse<TResponseData>> PutAsync<TResponseData>(string? requestUri, HttpContent? content = null) where TResponseData : class
    {
        var httpResponse = await _httpClient.PutAsync(requestUri, content);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }


    protected async Task<ApiResponse> PutAsJsonAsync<TActionData>(string? requestUri, TActionData value)
    {
        var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, value);

        try
        {
            return await httpResponse.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ApiResponse.ByException(ex);
        }
    }

    protected async Task<ApiResponse<TResponseData>> PutAsJsonAsync<TActionData, TResponseData>(string? requestUri, TActionData value) where TResponseData : class
    {
        var httpResponse = await _httpClient.PutAsJsonAsync(requestUri, value);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }


    protected async Task<ApiResponse> DeleteAsync(string? requestUri)
    {
        var httpResponse = await _httpClient.DeleteAsync(requestUri);

        try
        {
            return await httpResponse.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ApiResponse.ByException(ex);
        }
    }

    protected async Task<ApiResponse<TResponseData>> DeleteAsync<TResponseData>(string? requestUri) where TResponseData : class
    {
        var httpResponse = await _httpClient.DeleteAsync(requestUri);

        try
        {
            return await httpResponse.ReadAsStringAsync<TResponseData>();
        }
        catch (Exception ex)
        {
            return ApiResponse<TResponseData>.ByException(ex);
        }
    }
}


public record ApiResponse(bool IsSuccess, HttpStatusCode StatusCode, string ResponseData)
{
    public static ApiResponse ByException(Exception ex)
    {
        return new (false, HttpStatusCode.BadRequest, ex.Message);
    }
}

public record ApiResponse<TResponseData>(bool IsSuccess, HttpStatusCode StatusCode, TResponseData Response, string ResponseData) : ApiResponse(IsSuccess, StatusCode, ResponseData) where TResponseData : class
{
    public new static ApiResponse<TResponseData> ByException(Exception ex)
    {
        return new(false, HttpStatusCode.BadRequest, null!, ex.Message);
    }
}


public static class BaseServiceExtensions
{
    public static async Task<ApiResponse<TResponseData>> ReadAsStringAsync<TResponseData>(this HttpResponseMessage message) where TResponseData : class
    {
        var responseData = await message.Content.ReadAsStringAsync();

        return message.IsSuccessStatusCode
            ? new (true, message.StatusCode, JsonConvert.DeserializeObject<TResponseData>(responseData)!, responseData)
            : new (false, message.StatusCode, null!, responseData);
    }

    public static async Task<ApiResponse> ReadAsStringAsync(this HttpResponseMessage message)
    {
        var responseData = await message.Content.ReadAsStringAsync();
        return new (message.IsSuccessStatusCode, message.StatusCode, responseData);
    }
}