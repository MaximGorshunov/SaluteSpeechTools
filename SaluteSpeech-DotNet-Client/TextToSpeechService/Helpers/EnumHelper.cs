using System.ComponentModel;
using System.Reflection;

namespace TextToSpeechService.Helpers;

internal static class EnumHelper
{
    /// <summary>
    /// Returns the description of an enum.
    /// </summary>
    /// <param name="enumValue">enum value</param>
    /// <returns></returns>
    public static string GetEnumDescription(Enum enumValue)
    {
        FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        if (fieldInfo == null) return enumValue.ToString();
        return fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] { Length: > 0 } attributes ? attributes[0].Description : enumValue.ToString();
    }
}
