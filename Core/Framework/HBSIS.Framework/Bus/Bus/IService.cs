namespace HBSIS.Framework.Bus.Bus
{
    public interface IService
    {
    }

    public interface IService<TMessage>
    {
        void StoreMessage(TMessage message);
    }
}