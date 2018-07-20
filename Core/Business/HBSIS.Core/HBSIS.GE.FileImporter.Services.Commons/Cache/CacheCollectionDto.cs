using HBSIS.GE.FileImporter.Services.Commons.Base.Cache;
using System;
using System.Collections.Generic;

namespace HBSIS.GE.FileImporter.Services.Commons.Cache
{
    public class CacheCollectionDto : CacheDto<CacheCollectionDto>
    {
        public List<Tuple<string, string>> Dtos { get; set; }
    }
}