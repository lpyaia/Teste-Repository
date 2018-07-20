using System;
using System.Globalization;

namespace HBSIS.Framework.Commons.Helper
{
    public static class ConvertHelper
    {
        public static bool ToBoolean(string input)
        {
            bool result;

            if (bool.TryParse(input, out result))
            {
                return result;
            }

            return false;
        }

        public static decimal ToDecimal(string input)
        {
            return ToDecimal(input, CultureInfo.CurrentCulture);
        }

        public static string ToString(object value)
        {
            return value?.ToString();
        }

        public static DateTime FromOaDate(string value)
        {
            double d;

            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out d);

            if (d != 0)
                return DateTime.FromOADate(d);

            return DateTime.MinValue;
        }

        public static DateTime ToDateTime(string value)
        {
            DateTime date;

            if (DateTime.TryParse(value, out date)) return date;

            return DateTime.MinValue;
        }

        public static decimal ToDecimal(string input, IFormatProvider provider)
        {
            decimal result;

            if (decimal.TryParse(input, NumberStyles.Number, provider, out result))
            {
                return result;
            }

            return 0;
        }

        public static double ToDouble(string input)
        {
            return ToDouble(input, CultureInfo.CurrentCulture);
        }

        public static double ToDouble(string input, IFormatProvider provider)
        {
            double result;

            if (double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, provider, out result))
            {
                return result;
            }

            return 0;
        }

        public static double? ToNullableDouble(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToDouble(input);
        }

        public static double? ToNullableDouble(string input, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToDouble(input, provider);
        }
        public static short ToShort(string input)
        {
            return ToShort(input, CultureInfo.CurrentCulture);
        }

        public static short ToShort(string input, IFormatProvider provider)
        {
            short result;

            if (short.TryParse(input, NumberStyles.Integer, provider, out result))
            {
                return result;
            }

            return 0;
        }

        public static int ToInt(string input)
        {
            return ToInt(input, CultureInfo.CurrentCulture);
        }

        public static int ToInt(string input, IFormatProvider provider)
        {
            int result;

            if (int.TryParse(input, NumberStyles.Integer, provider, out result))
            {
                return result;
            }

            return 0;
        }

        public static int? ToNullableInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToInt(input);
        }

        public static int? ToNullableInt(string input, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToInt(input, provider);
        }

        public static double? ToCoordinate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            var ret = ConvertHelper.ToNullableDouble(value, CultureInfo.InvariantCulture);

            if (ret == 0) return null;

            if (ret.Value < -90 || ret.Value > 90) return null;

            return ret;
        }
    }
}