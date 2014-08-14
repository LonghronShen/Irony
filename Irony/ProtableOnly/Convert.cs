#if PORTABLE
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{

    public static class Convert
    {

        private static Type GlobalConvertType;
        private static Dictionary<string, MethodInfo> Methods;

        static Convert()
        {
            GlobalConvertType = typeof(BitConverter).Assembly.GetTypes().FirstOrDefault(item => item.FullName.Contains("System.Convert"));
            if (GlobalConvertType == null)
            {
                throw new ArgumentException("Failed to load the original System.Convert type.");
            }
            Methods = GlobalConvertType.GetMethods(BindingFlags.Public | BindingFlags.Static).ToDictionary(mi => mi.Name);
        }

        #region Helper
        internal static object InternalDispacher(string methodName, params object[] parameters)
        {
            if (!Methods.ContainsKey(methodName, true))
            {
                throw new PlatformNotSupportedException("This function is not suppoted in the Portable Class Library.");
            }
            var method = Methods.FirstOrDefault(item => string.Equals(item.Key, methodName, StringComparison.CurrentCultureIgnoreCase)).Value;
            return method.Invoke(null, parameters.Where(item => item.GetType() != typeof(CultureInfo)).Select(item => item.GetType() == typeof(TypeCode) ? TypeCodeToType((TypeCode)item) : item).ToArray());
        }

        internal static Type TypeCodeToType(this TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    return typeof(object);
                case TypeCode.Boolean:
                    return typeof(bool);
                case TypeCode.Byte:
                    return typeof(byte);
                case TypeCode.Char:
                    return typeof(char);
                case TypeCode.DateTime:
                    return typeof(DateTime);
                case TypeCode.Decimal:
                    return typeof(decimal);
                case TypeCode.Double:
                    return typeof(double);
                case TypeCode.Int16:
                    return typeof(Int16);
                case TypeCode.Int32:
                    return typeof(Int32);
                case TypeCode.Int64:
                    return typeof(Int64);
                case TypeCode.Object:
                    return typeof(object);
                case TypeCode.SByte:
                    return typeof(sbyte);
                case TypeCode.Single:
                    return typeof(float);
                case TypeCode.String:
                    return typeof(string);
                case TypeCode.UInt16:
                    return typeof(UInt16);
                case TypeCode.UInt32:
                    return typeof(UInt32);
                case TypeCode.UInt64:
                    return typeof(UInt64);
                default:
                    var typeName = System.Enum.GetName(typeof(TypeCode), code);
                    var type = typeof(bool).Assembly.GetTypes().FirstOrDefault(item => item.FullName.Contains("System." + typeName));
                    return type;
            }
        }

        internal static string GetCallerName([CallerMemberName]string methodName = null)
        {
            return methodName == null ? "" : methodName;
        }
        #endregion

        internal static object ChangeType(object dValue, TypeCode DataType, CultureInfo cultureInfo)
        {
            return InternalDispacher(GetCallerName(), DataType);
        }

        internal static object ChangeType(object dValue, TypeCode DataType, IFormatProvider formatProvider)
        {
            return InternalDispacher(GetCallerName(), DataType, formatProvider);
        }

        internal static double ToDouble(string textValue, CultureInfo cultureInfo)
        {
            return (double)InternalDispacher(GetCallerName(), textValue, cultureInfo);
        }

        internal static object ToInt64(string textValue, Globalization.CultureInfo cultureInfo)
        {
            return InternalDispacher(GetCallerName(), textValue);
        }

        internal static object ToInt64(string textValue, int IntRadix)
        {
            return InternalDispacher(GetCallerName(), textValue);
        }

        internal static double ToDouble(object value)
        {
            return (double)InternalDispacher(GetCallerName(), value);
        }

        internal static Int64 ToInt64(object value)
        {
            return (Int64)InternalDispacher(GetCallerName(), value);
        }

        internal static Int32 ToInt32(string textValue, CultureInfo cultureInfo)
        {
            return (Int32)InternalDispacher(GetCallerName(), textValue, cultureInfo);
        }

        internal static Int32 ToInt32(string textValue, int radix)
        {
            return (Int32)InternalDispacher(GetCallerName(), textValue, radix);
        }

        internal static Int32 ToInt32(string textValue, NumberFormatInfo numberFormatInfo)
        {
            return (Int32)InternalDispacher(GetCallerName(), textValue, (IFormatProvider)numberFormatInfo);
        }

        internal static UInt32 ToUInt32(string textValue, int radix)
        {
            return (UInt32)InternalDispacher(GetCallerName(), textValue, radix);
        }

        internal static UInt64 ToUInt64(string textValue, CultureInfo cultureInfo)
        {
            return (UInt64)InternalDispacher(GetCallerName(), textValue, cultureInfo);
        }

        internal static UInt64 ToUInt64(string textValue, int radix)
        {
            return (UInt64)InternalDispacher(GetCallerName(), textValue, radix);
        }

        internal static UInt64 ToUInt64(string textValue)
        {
            return (UInt64)InternalDispacher(GetCallerName(), textValue);
        }

    }

}
#endif