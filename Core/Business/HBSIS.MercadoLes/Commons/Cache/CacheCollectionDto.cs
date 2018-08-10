using HBSIS.MercadoLes.Commons.Base.Cache;
using System;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Commons.Cache
{
    public class CacheCollectionDto : CacheDto<CacheCollectionDto>
    {
        public List<Tuple<string, string>> Dtos { get; set; }
    }
}