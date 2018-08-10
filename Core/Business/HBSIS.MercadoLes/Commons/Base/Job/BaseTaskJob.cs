using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HBSIS.MercadoLes.Commons.Base.Job
{
    

    public abstract class BaseTaskJob : ITaskJob
    {
        private CancellationTokenSource _tokenSource;
        private readonly Stopwatch _watcher;

        public BaseTaskJob()
        {
            _tokenSource = new CancellationTokenSource();
            _watcher = Stopwatch.StartNew();
        }

        protected List<Task> Tasks { get; } = new List<Task>();

        protected CancellationToken Token
        {
            get { return _tokenSource.Token; }
        }

        public void Start()
        {
            _watcher?.Restart();
            StartInternal();
        }

        protected virtual void StartInternal()
        {
        }

        public void Stop()
        {
            try
            {
                StopInternal();

                _tokenSource.Cancel();
                Task.WaitAll(Tasks.ToArray());
            }
            catch
            {
            }
            finally
            {
                _tokenSource.Dispose();
            }

            if (_watcher != null && _watcher.IsRunning)
            {
                _watcher.Stop();
                LoggerHelper.Info($"Finalizado: {_watcher.Elapsed}");
            }
        }

        protected virtual void StopInternal()
        {
        }
    }
}