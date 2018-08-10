using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;
using System.Collections.Generic;

namespace HBSIS.Framework.Bus.Cache
{
    public static class CacheExtensions
    {
        #region ToMessage

        public static CacheMessage ToMessage<T>(this T dto)
           where T : IDto
        {
            if (dto == null) return null;
            return new CacheMessage(dto);
        }

        public static IEnumerable<CacheMessage> ToMessages<T>(this IEnumerable<T> items)
            where T : IDto
        {
            if (items == null) yield break;

            foreach (var item in items)
            {
                yield return item.ToMessage();
            }
        }

        #endregion ToMessage
    }
}