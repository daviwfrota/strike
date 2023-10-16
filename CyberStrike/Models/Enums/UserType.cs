
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CyberStrike.Models.Enums;
[JsonConverter(typeof(StringEnumConverter))]
public enum UserType
{
    Client,
    Admin,
    Operator,
}