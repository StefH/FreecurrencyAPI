#if NET8_0_OR_GREATER
using System.Text.RegularExpressions;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace FreecurrencyAPI.Logging;

internal class CustomHttpLogger : IHttpClientLogger
{
    
    //private const string ApiKey = "apikey=***";
    //private static readonly Regex ApiKeyPattern = new(@"(?i)Apikey=[^&]*", RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));

    private readonly ILogger<CustomHttpLogger> _logger;
    private readonly IRequestPathAndQueryReplacer _requestPathAndQueryReplacer;

    public CustomHttpLogger(ILogger<CustomHttpLogger> logger, IRequestPathAndQueryReplacer requestPathAndQueryReplacer)
    {
        _logger = Guard.NotNull(logger);
        _requestPathAndQueryReplacer = Guard.NotNull(requestPathAndQueryReplacer);
    }

    /*
     * [07:26:36 INF] Sending HTTP request GET https://api.freecurrencyapi.com/v1/latest?base_currency=USD&currencies=EUR%2CCAD&apikey=fca_live_0123456789012345678901234567890123456789
[07:26:36 INF] Received HTTP response headers after 40.4108ms - 200
[07:26:36 INF] End processing HTTP request after 41.9745ms - 200
[07:26:36 INF] GetLatestExchangeRatesAsync|{"Data":{"CAD":1.3462901624,"EUR":0.9128901447},"Rate":1.3462901624}
     */

    public object? LogRequestStart(HttpRequestMessage request)
    {
        _logger.LogInformation(
            "Sending HTTP request {0} {1}{2}",
            request.Method,
            request.RequestUri?.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped),
            _requestPathAndQueryReplacer.Replace(request.RequestUri!.PathAndQuery)
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
            "Received HTTP {0} {1} after {2}ms - {3}",
            (int)response.StatusCode,
            response.StatusCode,
            elapsed.TotalMilliseconds.ToString("F1"),
            response.StatusCode
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
            _requestPathAndQueryReplacer.Replace(request.RequestUri!.PathAndQuery),
            elapsed.TotalMilliseconds.ToString("F1")
        );
    }

    //private string ReplaceApiKeyFromPathAndQuery(string url)
    //{
    //    return ApiKeyPattern.Replace(url, ApiKey);
    //}
}

#endif