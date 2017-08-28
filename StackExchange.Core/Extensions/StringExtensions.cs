namespace StackExchange.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string value)
            => string.IsNullOrEmpty(value);
    }
}
