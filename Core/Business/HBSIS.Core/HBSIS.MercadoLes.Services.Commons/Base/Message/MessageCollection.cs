using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Message;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Commons.Base.Message
{
    public class MessageCollection : Queue<ISpecializedMessage>
    {
        public static MessageCollection Empty => new MessageCollection();

        public void Enqueue<T>(IEnumerable<T> collection)
            where T : ISpecializedMessage
        {
            if (collection == null) return;

            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }
    }
}