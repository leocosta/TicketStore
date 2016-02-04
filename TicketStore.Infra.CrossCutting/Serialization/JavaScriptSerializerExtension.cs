using System.Web.Script.Serialization;

namespace TicketStore.Infra.CrossCutting.Serialization
{
    public static class JavaScriptSerializerExtension
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public static string Serialize(this object value)
        {
            return Serializer.Serialize(value);
        }

        public static T Deserialize<T>(this string value)
        {
            return Serializer.Deserialize<T>(value);
        }
    }
}
