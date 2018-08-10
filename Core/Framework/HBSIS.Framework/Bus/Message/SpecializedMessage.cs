namespace HBSIS.Framework.Bus.Message
{
    public abstract class SpecializedMessage<T> : GeneralMessage, ISpecializedMessage
        where T : class, IPublishMessage
    {
        public SpecializedMessage()
            : base(typeof(T).Name)
        {
            ContextName = typeof(T).Name.Replace("Message", "");           
        }

        public virtual string ContextName { get; }
    }
}