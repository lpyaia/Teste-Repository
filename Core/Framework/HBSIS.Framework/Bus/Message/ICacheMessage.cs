namespace HBSIS.Framework.Bus.Message
{
    public interface ICacheMessage
    {
        string ContentType { get; set; }
        string Content { get; set; }
    }
}