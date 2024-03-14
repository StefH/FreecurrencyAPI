#if NET8_0_OR_GREATER
namespace FreecurrencyAPI.Logging;

internal interface IRequestPathAndQueryReplacer
{
    string Replace(string pathAndQuery);
}
#endif