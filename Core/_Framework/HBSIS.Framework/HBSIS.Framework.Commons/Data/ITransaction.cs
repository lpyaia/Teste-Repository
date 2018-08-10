using System;

namespace HBSIS.Framework.Commons.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();
    }
}