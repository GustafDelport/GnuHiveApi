using System.Text.Json.Serialization;

namespace GnuHiveApi.Domain.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GreenHouseModulesEnum
{
    Fan,
    Light,
    Pump
}