using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CyberStrike.Models.Enums;
[JsonConverter(typeof(StringEnumConverter))]
public enum TokenType
{
    REFRESH,
    ACTIVATE_ACCOUNT,
    LOGIN_SECURITY
}