#if NET8_0_OR_GREATER
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using FreecurrencyAPI.Options;
using Microsoft.Extensions.Options;

namespace FreecurrencyAPI.Logging;

internal class RequestPathAndQueryReplacer : IRequestPathAndQueryReplacer
{
    private readonly ConcurrentDictionary<Lazy<Regex>, string> _pathAndQueryReplacements = new();

    public RequestPathAndQueryReplacer(IOptions<FreecurrencyAPIOptions> options)
    {
        var requestPathAndQueryReplacements = Guard.NotNull(options.Value).RequestPathAndQueryReplacements;
        if (requestPathAndQueryReplacements != null)
        {
            foreach (var replacement in requestPathAndQueryReplacements)
            {
                _pathAndQueryReplacements.TryAdd(new Lazy<Regex>(() => new Regex(replacement.Key, RegexOptions.Compiled, TimeSpan.FromMilliseconds(100))), replacement.Value);
            }
        }
    }

    public string Replace(string pathAndQuery)
    {
        var result = pathAndQuery;

        foreach (var (regex, replacement) in _pathAndQueryReplacements)
        {
            result = regex.Value.Replace(result, replacement);
        }

        return result;
    }
}
#endif