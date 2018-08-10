namespace HBSIS.Framework.Bus.Message
{
    public interface ICallbackMessage : IPublishMessage
    {
        string Token { get; set; }
    }
}