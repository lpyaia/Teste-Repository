using HBSIS.Framework.Bus.Message;

namespace HBSIS.Framework.Bus.Bus
{
    public interface IDto : IBusMessage
    {
        StatusDto Action { get; set; }
    }
}