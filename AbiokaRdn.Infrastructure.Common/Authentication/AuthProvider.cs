using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbiokaRdn.Infrastructure.Common.Authentication
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthProvider
    {
        Local,
        Google,
        Facebook
    }
}
