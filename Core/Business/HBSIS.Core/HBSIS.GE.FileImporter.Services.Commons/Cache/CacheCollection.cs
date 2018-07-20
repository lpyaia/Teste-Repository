using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using System;
using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Commons.Cache
{
    public class CacheCollection : Queue<IDto>, IDto
    {
        public static CacheCollection Empty => new CacheCollection();

        public Guid Id { get; set; }

        public StatusDto Action { get; set; }

        public void Enqueue<T>(IEnumerable<T> collection)
            where T : IDto
        {
            if (collection == null) return;

            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }
    }
}