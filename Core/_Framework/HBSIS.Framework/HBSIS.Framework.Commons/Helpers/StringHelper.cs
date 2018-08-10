using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace HBSIS.Framework.Commons.Helper
{
    public static class StringHelper
    {
        public static string RemoveDiacritics(this string value)
        {
            if (value == null) return null;

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveNumerics(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\d", "");
        }

        public static string RemoveAphabetics(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\D", "");
        }

        public static string RemoveSpaces(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\s+", "");
        }

        public static string RemoveDoubleSpaces(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\s+", " ");
        }

        public static string DiscardLeadingZeroes(string valor)
        {
            long n;

            if (long.TryParse(valor, out n))
            {
                return n.ToString();
            }

            return valor;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}