using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Commons.Context
{
    public class ThreadContext : IApplicationContext
    {
        [ThreadStatic]
        private static Dictionary<string, object> values;

        private Dictionary<string, object> Values
        {
            get { return values ?? (values = new Dictionary<string, object>()); }
        }

        public object this[string key]
        {
            get
            {
                if (Values.ContainsKey(key))
                {
                    return Values[key];
                }

                return null;
            }
            set
            {
                Values[key] = value;
            }
        }
    }
}