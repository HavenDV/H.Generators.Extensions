using Microsoft.CodeAnalysis;

namespace H.Generators.Extensions;

/// <summary>
/// 
/// </summary>
public static class ConvertExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="typedConstant"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool ToBoolean(this TypedConstant typedConstant, bool defaultValue = false)
    {
        if (typedConstant.Value == null)
        {
            return defaultValue;
        }

        return (bool)typedConstant.Value!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typedConstant"></param>
    /// <returns></returns>
    public static bool? ToNullableBoolean(this TypedConstant typedConstant)
    {
        if (typedConstant.Value == null)
        {
            return null;
        }

        return (bool)typedConstant.Value!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typedConstant"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToEnum<T>(this TypedConstant typedConstant, T defaultValue) where T : Enum
    {
        return (T)(typedConstant.Value ?? defaultValue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typedConstant"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ToEnum<T>(this TypedConstant typedConstant) where T : struct, Enum
    {
        if (typedConstant.Value == null)
        {
            return null;
        }

        return (T)typedConstant.Value;
    }
}
