using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FreecurrencyAPI.Models;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[PublicAPI]
public static class CurrencyCodes
{
    private static Lazy<string[]> _supportedCodes = new(() =>
    {
        return typeof(CurrencyCodes)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi is { IsLiteral: true, IsInitOnly: false } && fi.FieldType == typeof(string))
            .Select(fi => (string?)fi.GetRawConstantValue())
            .OfType<string>()
            .ToArray();
    });

    // Australian Dollar
    public const string AUD = "AUD";

    // Brazilian Real
    public const string BRL = "BRL";

    // Bulgarian Lev
    public const string BGN = "BGN";

    // Canadian Dollar
    public const string CAD = "CAD";

    // Swiss Franc
    public const string CHF = "CHF";

    // Chinese Yuan
    public const string CNY = "CNY";

    // Czech Republic Koruna
    public const string CZK = "CZK";

    // Danish Krone
    public const string DKK = "DKK";

    // Euro
    public const string EUR = "EUR";

    // British Pound Sterling
    public const string GBP = "GBP";

    // Croatian Kuna
    public const string HRK = "HRK";

    // Hungarian Forint
    public const string HUF = "HUF";

    // Indonesian Rupiah
    public const string IDR = "IDR";

    // Israeli New Sheqel
    public const string ILS = "ILS";

    // Indian Rupee
    public const string INR = "INR";

    // Icelandic Króna
    public const string ISK = "ISK";

    // Japanese Yen
    public const string JPY = "JPY";

    // South Korean Won
    public const string KRW = "KRW";

    // Mexican Peso
    public const string MXN = "MXN";

    // Malaysian Ringgit
    public const string MYR = "MYR";

    // Norwegian Krone
    public const string NOK = "NOK";

    // New Zealand Dollar
    public const string NZD = "NZD";

    // Philippine Peso
    public const string PHP = "PHP";

    // Polish Zloty
    public const string PLN = "PLN";

    // Romanian Leu
    public const string RON = "RON";

    // Russian Ruble
    public const string RUB = "RUB";

    // Swedish Krona
    public const string SEK = "SEK";

    // Singapore Dollar
    public const string SGD = "SGD";

    // Thai Baht
    public const string THB = "THB";

    // Turkish Lira
    public const string TRY = "TRY";

    // US Dollar
    public const string USD = "USD";

    // South African Rand
    public const string ZAR = "ZAR";

    /// <summary>
    /// Checks if the given currency code is supported.
    /// </summary>
    /// <param name="code">The code</param>
    /// <returns><c>true</c> when supported, else <c>false</c></returns>
    public static bool IsSupported(string code)
    {
        return _supportedCodes.Value.Contains(code);
    }
}