using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Bus.Message;

namespace HBSIS.Framework.Bus.Cache
{
    public interface ICacher 
    {
        void StoreCache(CacheMessage message);

        void StoreCache(IDto dto);
    }
}