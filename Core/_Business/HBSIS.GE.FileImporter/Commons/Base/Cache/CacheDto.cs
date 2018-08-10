﻿using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;

namespace HBSIS.GE.FileImporter.Services.Commons.Base.Cache
{
    public class CacheDto<T> : IDto
         where T : CacheDto<T>
    {
        public StatusDto Action { get; set; }
    }
}