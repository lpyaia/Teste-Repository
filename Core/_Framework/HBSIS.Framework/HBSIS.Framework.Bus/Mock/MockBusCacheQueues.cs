using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Bus.Mock
{
    public static class MockBusCacheQueues
    {
        public static List<T> GetAll<T>(string contextName)
             where T : class
        {
            var messages = MockBusQueues.Gets<CacheMessage>(contextName);
            return messages.Where(x => x.ContentType == typeof(T).Name).Select(x => x.Content.JsonDeserialize<T>()).ToList();
        }

        public static T GetFirst<T>(string contextName)
           where T : class
        {
            var values = GetAll<T>(contextName);

            return values.FirstOrDefault();
        }

        public static T GetLast<T>(string contextName)
            where T : class
        {
            var values = GetAll<T>(contextName);

            return values.LastOrDefault();
        }
    }
}