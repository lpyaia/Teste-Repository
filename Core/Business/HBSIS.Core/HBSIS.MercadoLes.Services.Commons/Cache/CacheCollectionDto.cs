using HBSIS.MercadoLes.Services.Commons.Base.Cache;
using System;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Commons.Cache
{
    public class CacheCollectionDto : CacheDto<CacheCollectionDto>
    {
        public List<Tuple<string, string>> Dtos { get; set; }
    }
}