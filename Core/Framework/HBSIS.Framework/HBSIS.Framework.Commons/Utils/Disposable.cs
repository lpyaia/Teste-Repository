using System;

namespace HBSIS.Framework.Commons.Utils
{
    public class Disposable : IDisposable
    {
        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                Disposed = true;
            }
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}