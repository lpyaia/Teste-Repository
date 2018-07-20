using HBSIS.Framework.Commons.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Commons.Helper
{
    public static class EnumHelper
    {
        public static TEnum ParseTo<TEnum>(string value)
            where TEnum : struct
        {
            TEnum ret;

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (Enum.TryParse<TEnum>(value, true, out ret))
                {
                    return ret;
                }

                var values = Enum.GetValues(typeof(TEnum));

                if (values != null)
                {
                    foreach (var item in values)
                    {
                        var enumerator = item as Enum;

                        if (enumerator != null)
                        {
                            if (enumerator.GetNames().Any(x => x.Equals(value, StringComparison.InvariantCultureIgnoreCase)))
                                return (TEnum)item;
                        }
                    }
                }
            }

            return default(TEnum);
        }

        public static IEnumerable<string> GetNames(this Enum value)
        {
            if (value != null)
            {
                var member = value.GetType().GetMember(value.ToString());

                if (member != null && member.Length > 0)
                {
                    var attrs = member.FirstOrDefault()?.GetCustomAttributes(typeof(NameAttribute), false);

                    if (attrs != null)
                    {
                        foreach (NameAttribute item in attrs)
                        {
                            yield return item.Name;
                        }
                    }
                }

                yield return value.ToString();
            }
        }

        public static string GetName(this Enum value)
        {
            return GetNames(value).FirstOrDefault();
        }

        public static string GetDisplayString(this Enum value)
        {
            var member = value?.GetType().GetMember(value.ToString());

            if (member != null && member.Length > 0)
            {
                var attr = member?.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayStringAttribute), false)?.FirstOrDefault() as DisplayStringAttribute;

                if (attr != null)
                {
                    return attr.Name;
                }
            }

            return value?.ToString();
        }
    }
}