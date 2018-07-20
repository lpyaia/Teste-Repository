namespace HBSIS.Framework.Bus.Message
{
    public interface ISpecializedMessage : IPublishMessage, ICallbackMessage
    {
        string ContextName { get; }
    }
}