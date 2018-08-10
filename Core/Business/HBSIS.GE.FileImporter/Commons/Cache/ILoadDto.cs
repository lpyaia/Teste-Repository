using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Bus;
using System;

namespace HBSIS.GE.FileImporter.Services.Commons.Cache
{
    public interface ILoadDto : IDto
    {
        Guid IdRota { get; }
    }
}