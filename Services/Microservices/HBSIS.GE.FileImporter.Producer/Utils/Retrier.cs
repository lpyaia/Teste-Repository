using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Producer.Utils
{
    public class Retrier<TResult>
    {
        public TResult Try(Func<TResult> func,
            int maxRetries)
        {
            TResult returnValue = default(TResult);
            int numTries = 0;
            bool succeeded = false;
            while (numTries < maxRetries)
            {
                try
                {
                    returnValue = func();
                    succeeded = true;
                }
                catch (Exception)
                {
                    //todo: figure out what to do here
                }
                finally
                {
                    numTries++;
                }
                if (succeeded)
                    return returnValue;
            }
            return default(TResult);
        }

        public TResult TryWithDelay(Func<TResult> func, int maxRetries, int delayInMilliseconds)
        {
            TResult returnValue = default(TResult);
            int numTries = 0;
            bool succeeded = false;

            while (numTries < maxRetries)
            {
                try
                {
                    returnValue = func();
                    succeeded = true;
                }
                catch (Exception)
                {
                    //todo: figure out what to do here
                }
                finally
                {
                    numTries++;
                }

                if (succeeded)
                    return returnValue;

                System.Threading.Thread.Sleep(delayInMilliseconds);
            }
            return default(TResult);
        }

        public bool TryWithDelay(Func<bool> func, int maxRetries, int delayInMilliseconds)
        {
            int numTries = 1;
            bool succeeded = false;

            while (numTries <= maxRetries)
            {
                LoggerHelper.Info($"Tentativa de envio {numTries}/{maxRetries}...\n");

                try
                {
                    succeeded = func();
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error($"Exception: {ex.Message}");
                }
                finally
                {
                    numTries++;
                }

                if (succeeded)
                    return true;
                
                System.Threading.Thread.Sleep(delayInMilliseconds);
            }

            return false;
        }
    }
}
