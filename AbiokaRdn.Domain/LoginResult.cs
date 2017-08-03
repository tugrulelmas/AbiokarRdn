using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbiokaRdn.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LoginResult
    {
        Successful,
        WrongPassword,
        UserIsNotActive,
        UnverifiedEmail
    }
}