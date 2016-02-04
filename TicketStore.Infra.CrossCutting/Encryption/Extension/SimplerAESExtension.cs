namespace TicketStore.Infra.CrossCutting.Encryption.Extension
{
    public static class SimplerAESExtension
    {
        private readonly static SimplerAES SimplerAES = new SimplerAES();

        public static string Encrypt(this string value)
        {
            return SimplerAES.Encrypt(value);
        }

        public static string Decrypt(this string value)
        {
            return SimplerAES.Decrypt(value);
        }
    }
}
