using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HBSIS.Framework.Commons.Helper
{
    public static class MemberInfoHelper
    {
        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (exp == null || !TryGetMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryGetMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        public static TProperty GetPropertyValue<T, TProperty>(this T obj, Expression<Func<T, TProperty>> path)
        {
            var ret = default(TProperty);

            if (obj != null && path != null)
            {
                var propertyPath = GetPropertyName<T, TProperty>(path);
                ret = (TProperty)GetPropertyValue(obj, propertyPath);
            }

            return ret;
        }

        public static void SetPropertyValue<T, TProperty>(this T obj, Expression<Func<T, TProperty>> path, TProperty value)
        {
            if (obj != null && path != null)
            {
                var propertyPath = GetPropertyName<T, TProperty>(path);
                SetPropertyValue(obj, propertyPath, value);
            }
        }

        private static object GetPropertyValue(this object obj, string fullPropertyName)
        {
            object propertyValue = null;

            if (fullPropertyName.IndexOf(".") < 0)
            {
                var objType = obj.GetType();
                propertyValue = objType.GetProperty(fullPropertyName)?.GetValue(obj, null);
                return propertyValue;
            }

            var properties = fullPropertyName.Split('.').ToList();

            object midPropertyValue = obj;

            while (properties.Count > 0)
            {
                var propertyName = properties.FirstOrDefault();
                properties.Remove(propertyName);

                propertyValue = GetPropertyValue(midPropertyValue, propertyName);
                midPropertyValue = propertyValue;
            }

            return propertyValue;
        }

        private static void SetPropertyValue(this object obj, string fullPropertyName, object value)
        {
            if (fullPropertyName.IndexOf(".") < 0)
            {
                var objType = obj.GetType();
                objType.GetProperty(fullPropertyName)?.SetValue(obj, value, null);
                return;
            }

            var properties = fullPropertyName.Split('.').ToList();

            object midPropertyValue = obj;

            while (properties.Count > 0)
            {
                var propertyName = properties.FirstOrDefault();
                properties.Remove(propertyName);

                if (properties.Count == 0)
                {
                    SetPropertyValue(midPropertyValue, propertyName, value);
                    return;
                }

                midPropertyValue = GetPropertyValue(midPropertyValue, propertyName);
            }
        }

        private static bool TryGetMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null) { return true; }

            memberExp = (exp as UnaryExpression)?.Operand as MemberExpression;

            return memberExp != null;
        }
    }
}