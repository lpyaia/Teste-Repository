using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Data.Mongo;
using System;

namespace HBSIS.MercadoLes.Commons.Base.Cache
{
    public class CacheEntity<T> : MongoEntity<T>
        where T : CacheEntity<T>
    {
        public StatusDto Action { get; set; }

        public CacheEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}