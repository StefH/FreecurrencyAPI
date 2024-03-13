#if NET8_0_OR_GREATER
using System.Text.RegularExpressions;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace FreecurrencyAPI.Logging;

internal class CustomHttpLogger : IHttpClientLogger
{
    private const string ApiKey = "apikey=***";
    private static readonly Regex ApiKeyPattern = new(@"apikey=[^&]*", RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));

    private readonly ILogger<CustomHttpLogger> _logger;

    public CustomHttpLogger(ILogger<CustomHttpLogger> logger)
    {
        _logger = Guard.NotNull(logger);
    }

    public object? LogRequestStart(HttpRequestMessage request)
    {
        _logger.LogInformation(
            "Sending '{0}' to '{1}{2}'",
            request.Method,
            request.RequestUri?.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped),
            ReplaceApiKeyFromPathAndQuery(request.RequestUri!.PathAndQuery)
        );

        return null;
    }

    public void LogRequestStop(
        object? context,
        HttpRequestMessage request,
        HttpResponseMessage response,
        TimeSpan elapsed)
    {
        _logger.LogInformation(
            "Received '{0} {1}' after {2}ms",
            (int)response.StatusCode,
            response.StatusCode,
            elapsed.TotalMilliseconds.ToString("F1")
        );
    }

    public void LogRequestFailed(
        object? context,
        HttpRequestMessage request,
        HttpResponseMessage? response,
        Exception exception,
        TimeSpan elapsed)
    {
        _logger.LogError(
            exception,
            "Request towards '{Request.Host}{Request.Path}' failed after {Response.ElapsedMilliseconds}ms",
            request.RequestUri?.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped),
            ReplaceApiKeyFromPathAndQuery(request.RequestUri!.PathAndQuery),
            elapsed.TotalMilliseconds.ToString("F1")
        );
    }

    private static string ReplaceApiKeyFromPathAndQuery(string url)
    {
        return ApiKeyPattern.Replace(url, ApiKey);
    }
}

#endif