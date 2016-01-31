namespace TicketStore.Infra.CrossCutting.Helpers
{
    public static class StringExtension
    {
        public static string GetLast(this string source, int tailLength)
        {
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }
    }
}
